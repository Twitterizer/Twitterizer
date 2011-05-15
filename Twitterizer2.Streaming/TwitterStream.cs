using Twitterizer2.Streaming;
//-----------------------------------------------------------------------
// <copyright file="TwitterStream.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
// 
//  Copyright (c) 2010/2011, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The Twitter Stream class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Streaming
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
using Twitterizer.Core;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public enum StopReasons
    {
        StoppedByRequest,
        WebConnectionFailed,
        Unknown
    }

    public delegate void InitUserStreamCallback(TwitterIdCollection friendIds);

    public delegate void StatusCreatedCallback(TwitterStatus status);

    public delegate void StatusDeletedCallback(TwitterStreamDeletedStatus status);

    public delegate void DirectMessageCreatedCallback(TwitterDirectMessage status);

    public delegate void DirectMessageDeletedCallback(TwitterStreamDeletedDirectMessage status);

    public delegate void EventCallback(TwitterStreamEvent eventDetails);

    /// <summary>
    /// The TwitterStream class. Provides an interface to real-time status changes.
    /// </summary>
    public class TwitterStream : IDisposable
    {
        private InitUserStreamCallback friendsCallback;
        private StatusCreatedCallback statusCreatedCallback;
        private StatusDeletedCallback statusDeletedCallback;
        private DirectMessageCreatedCallback directMessageCreatedCallback;
        private DirectMessageDeletedCallback directMessageDeletedCallback;
        private EventCallback eventCallback;

        /// <summary>
        /// This value is set to true to indicate that the stream connection should be closed. 
        /// </summary>
        private bool stopReceived;

        /// <summary>
        /// The useragant which shall be used in connections to Twitter (a must in the specs of the API)
        /// </summary>
        private string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        public OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStream"/> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userAgent">The useragent string which shall include the version of your client.</param>
        public TwitterStream(OAuthTokens tokens, string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                this.UserAgent = string.Format(
                    CultureInfo.InvariantCulture, 
                    "Twitterizer/{0}", 
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            }
            else
            {
                this.UserAgent = string.Format(
                    CultureInfo.InvariantCulture, 
                    "{0} (via Twitterizer/{1})", 
                    userAgent, 
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            }

            this.Tokens = tokens;
        }

        /// <summary>
        /// Starts the user stream.
        /// </summary>
        public IAsyncResult StartUserStream(
            InitUserStreamCallback friendsCallback,
            StatusCreatedCallback statusCreatedCallback, 
            StatusDeletedCallback statusDeletedCallback,
            DirectMessageCreatedCallback directMessageCreatedCallback,
            DirectMessageDeletedCallback directMessageDeletedCallback,
            EventCallback eventCallback
            )
        {
            WebRequestBuilder builder = new WebRequestBuilder(new Uri("https://userstream.twitter.com/2/user.json"), HTTPVerb.GET, this.Tokens);

            HttpWebRequest request = builder.PrepareRequest();
            request.KeepAlive = true;
            request.UserAgent = this.UserAgent;

            this.friendsCallback = friendsCallback;
            this.statusCreatedCallback = statusCreatedCallback; 
            this.statusDeletedCallback = statusDeletedCallback;
            this.directMessageCreatedCallback = directMessageCreatedCallback;
            this.directMessageDeletedCallback = directMessageDeletedCallback;
            this.eventCallback = eventCallback;

            return request.BeginGetResponse(StreamCallback, request);
        }

        /// <summary>
        /// The callback handler for all streams
        /// </summary>
        /// <param name="result">The result.</param>
        private void StreamCallback(IAsyncResult result)
        {
            HttpWebRequest req = (HttpWebRequest)result.AsyncState;

            using (var response = req.EndGetResponse(result))
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                // This will keep the count of open brackets
                // When { is encountered, the count is incremented
                // When } is encountered, the count is decremented
                int bracketCount = 0;

                // The blockBuilder will hold the string of the current block of json.
                StringBuilder blockBuilder = new StringBuilder();

                while (!this.stopReceived && !reader.EndOfStream)
                {
                    string lineOfData = reader.ReadLine();
                    
                    if (this.stopReceived)
                    {
                        continue;
                    }

                    for (int index = 0; index < lineOfData.Length; index++)
                    {
                        blockBuilder.Append(lineOfData[index]);

                        if (!new[] { '{', '}' }.Contains(lineOfData[index]))
                        {
                            continue;
                        }

                        if (lineOfData[index] == '{')
                        {
                            bracketCount++;
                        }

                        if (lineOfData[index] == '}')
                        {
                            bracketCount--;
                        }

                        if (bracketCount == 0)
                        {
                            Action<string> parseMethod = ParseMessage;
                            parseMethod.BeginInvoke(blockBuilder.ToString().Trim('\n'), null, null);
                            blockBuilder.Clear();
                        }
                    }
                }

                req.Abort();
                reader.Close();
                response.Close();
            }
        }

        /// <summary>
        /// Parses the message.
        /// </summary>
        /// <param name="p">The p.</param>
        private void ParseMessage(string p)
        {
            JObject obj = (JObject)JsonConvert.DeserializeObject(p);

            var friends = obj.SelectToken("friends", false);
            if (friends != null)
            {
                if (this.friendsCallback != null)
                {
                    this.friendsCallback(JsonConvert.DeserializeObject<TwitterIdCollection>(friends.ToString()));
                    return;
                }
            }

            var delete = obj.SelectToken("delete", false);
            if (delete != null)
            {
                var deletedstatus = delete.SelectToken("status", false);
                if (deletedstatus != null)
                {
                    if (this.statusDeletedCallback != null)
                    {
                        this.statusDeletedCallback(JsonConvert.DeserializeObject<TwitterStreamDeletedStatus>(deletedstatus.ToString()));
                        return;
                    }  
                    return;
                }

                var deleteddirectmessage = delete.SelectToken("directmessage", false);
                if (deleteddirectmessage != null)
                {
                    if (this.directMessageDeletedCallback != null)
                    {
                        this.directMessageDeletedCallback(JsonConvert.DeserializeObject<TwitterStreamDeletedDirectMessage>(deleteddirectmessage.ToString()));
                        return;
                    }
                    return;
                }
            }

            var events = obj.SelectToken("target_object", false);
            if (events != null)
            {
                if (this.eventCallback != null)
                {
                    this.eventCallback(JsonConvert.DeserializeObject<TwitterStreamEvent>(events.ToString()));
                    return;
                }
            }
            
            var status = obj.SelectToken("user", false);
            if (status != null)
            {
                if (this.statusCreatedCallback != null)
                {
                    this.statusCreatedCallback(JsonConvert.DeserializeObject<TwitterStatus>(obj.ToString()));
                    return;
                }
            }

            var directmessage = obj.SelectToken("direct_message", false);
            if (directmessage != null)
            {
                if (this.directMessageCreatedCallback != null)
                {
                    this.directMessageCreatedCallback(JsonConvert.DeserializeObject<TwitterDirectMessage>(obj.ToString()));
                    return;
                }
            }

            System.Diagnostics.Debug.WriteLine("Unknown Message: {0}", new object[] { obj.ToString() });
        }

       
        /// <summary>
        /// Ends the stream.
        /// </summary>
        public void EndStream()
        {
            EndStream(StopReasons.Unknown, "General reason");
        }

        public void EndStream(StopReasons reason, string description)
        {
            this.stopReceived = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.stopReceived = true;
        }
    }
}
