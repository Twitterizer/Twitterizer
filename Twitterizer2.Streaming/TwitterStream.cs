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
    using System.Net.Sockets;

    public enum StopReasons
    {
        StoppedByRequest,
        WebConnectionFailed,
        Unknown,
        Unauthorised,
        Forbidden,
        NotFound,
        NotAcceptable,
        TooLong,
        RangeUnacceptable,
        RateLimited,
        TwitterServerError,
        TwitterOverloaded
    }

    public delegate void InitUserStreamCallback(TwitterIdCollection friendIds);

    public delegate void StatusCreatedCallback(TwitterStatus status);

    public delegate void StatusDeletedCallback(TwitterStreamDeletedEvent status);

    public delegate void DirectMessageCreatedCallback(TwitterDirectMessage status);

    public delegate void DirectMessageDeletedCallback(TwitterStreamDeletedEvent status);

    public delegate void EventCallback(TwitterStreamEvent eventDetails);

    public delegate void StreamStoppedCallback(StopReasons stopreason);

    /// <summary>
    /// The TwitterStream class. Provides an interface to real-time status changes.
    /// </summary>
    public class TwitterStream : IDisposable
    {
        private InitUserStreamCallback friendsCallback;
        private StreamStoppedCallback streamStoppedCallback;
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
        /// Gets or sets the stream options.
        /// </summary>
        /// <value>The stream options.</value>
        public StreamOptions StreamOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStream"/> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="userAgent">The useragent string which shall include the version of your client.</param>
        /// <param name="streamoptions">The stream or user stream options to intially use when starting the stream.</param>
        public TwitterStream(OAuthTokens tokens, string userAgent, StreamOptions streamoptions)
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

            if (streamoptions != null)
                this.StreamOptions = streamoptions;
        }

        /// <summary>
        /// Starts the user stream.
        /// </summary>
        public IAsyncResult StartUserStream(
            InitUserStreamCallback friendsCallback,
            StreamStoppedCallback streamErrorCallback,
            StatusCreatedCallback statusCreatedCallback, 
            StatusDeletedCallback statusDeletedCallback,
            DirectMessageCreatedCallback directMessageCreatedCallback,
            DirectMessageDeletedCallback directMessageDeletedCallback,
            EventCallback eventCallback
            )
        {
            WebRequestBuilder builder = new WebRequestBuilder(new Uri("https://userstream.twitter.com/2/user.json"), HTTPVerb.GET, this.Tokens);

            PrepareStreamOptions(builder);

            if (this.StreamOptions != null && this.StreamOptions is UserStreamOptions)
            {
                if ((this.StreamOptions as UserStreamOptions).AllReplies)
                    builder.Parameters.Add("replies", "all");
            }

            HttpWebRequest request = builder.PrepareRequest();
          
            request.KeepAlive = true;
            request.UserAgent = this.UserAgent;

            this.friendsCallback = friendsCallback;
            this.streamStoppedCallback = streamErrorCallback;
            this.statusCreatedCallback = statusCreatedCallback; 
            this.statusDeletedCallback = statusDeletedCallback;
            this.directMessageCreatedCallback = directMessageCreatedCallback;
            this.directMessageDeletedCallback = directMessageDeletedCallback;
            this.eventCallback = eventCallback;
            this.stopReceived = false;
            return request.BeginGetResponse(StreamCallback, request);
        }

        /// <summary>
        /// Starts the user stream.
        /// </summary>
        public IAsyncResult StartPublicStream(            
            StreamStoppedCallback streamErrorCallback,
            StatusCreatedCallback statusCreatedCallback,
            StatusDeletedCallback statusDeletedCallback,
            EventCallback eventCallback
            )
        {
            WebRequestBuilder builder = new WebRequestBuilder(new Uri("https://stream.twitter.com/1/statuses/filter.json"), HTTPVerb.POST, this.Tokens);

            PrepareStreamOptions(builder);

            HttpWebRequest request = builder.PrepareRequest();
            
            request.KeepAlive = true;
            request.UserAgent = this.UserAgent;
            
            this.streamStoppedCallback = streamErrorCallback;
            this.statusCreatedCallback = statusCreatedCallback;
            this.statusDeletedCallback = statusDeletedCallback;
            this.eventCallback = eventCallback;
            this.stopReceived = false;
            return request.BeginGetResponse(StreamCallback, request);
        }

        private void PrepareStreamOptions(WebRequestBuilder builder)
        {
            if (this.StreamOptions != null)
            {
                if (this.StreamOptions.Count > 0)
                    builder.Parameters.Add("count", this.StreamOptions.Count.ToString());

                if (this.StreamOptions.Follow != null && this.StreamOptions.Follow.Count > 0)
                    builder.Parameters.Add("follow", string.Join(",", this.StreamOptions.Follow.ToArray()));

                if (this.StreamOptions.Locations != null && this.StreamOptions.Locations.Count > 0)
                    builder.Parameters.Add("locations", string.Join(",", this.StreamOptions.Locations.Select((x, r) => x.ToString()).ToArray()));

                if (this.StreamOptions.Track != null && this.StreamOptions.Track.Count > 0)
                    builder.Parameters.Add("track", string.Join(",", this.StreamOptions.Track.ToArray()));
            }
        }

        /// <summary>
        /// The callback handler for all streams
        /// </summary>
        /// <param name="result">The result.</param>
        private void StreamCallback(IAsyncResult result)
        {
            HttpWebRequest req = (HttpWebRequest)result.AsyncState;
            WebResponse response = null;
            try
            {
                response = req.EndGetResponse(result);

                if ((response as HttpWebResponse).StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        try
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
                            reader.Close();
                        }
                        catch
                        {
                            //Stream Closed/Failed
                            if (this.streamStoppedCallback != null)
                            {
                                if (!this.stopReceived)
                                    this.streamStoppedCallback(StopReasons.WebConnectionFailed);
                                else
                                    this.streamStoppedCallback(StopReasons.StoppedByRequest);
                            }
                        }
                    };
                }
            }
            catch (Exception e)
            {
                if (e is WebException)
                {
                    var we = e as WebException;
                    if (we.Response != null)
                    {
                        switch ((we.Response as HttpWebResponse).StatusCode)
                        {
                            case HttpStatusCode.Unauthorized:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.Unauthorised);
                                    break;
                                }
                            case HttpStatusCode.Forbidden:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.Forbidden);
                                    break;
                                }
                            case HttpStatusCode.NotFound:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.NotFound);
                                    break;
                                }
                            case HttpStatusCode.NotAcceptable:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.NotAcceptable);
                                    break;
                                }
                            case HttpStatusCode.RequestEntityTooLarge:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.TooLong);
                                    break;
                                }
                            case HttpStatusCode.RequestedRangeNotSatisfiable:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.RangeUnacceptable);
                                    break;
                                }
                            case (HttpStatusCode)420: //Rate Limited
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.RateLimited);
                                    break;
                                }
                            case HttpStatusCode.InternalServerError:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.TwitterServerError);
                                    break;
                                }
                            case HttpStatusCode.ServiceUnavailable:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.TwitterOverloaded);
                                    break;
                                }
                            default:
                                {
                                    if (this.streamStoppedCallback != null)
                                        this.streamStoppedCallback(StopReasons.Unknown);
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (this.streamStoppedCallback != null)
                            this.streamStoppedCallback(StopReasons.WebConnectionFailed);
                    }
                }
                else
                {
                    if (this.streamStoppedCallback != null)
                        this.streamStoppedCallback(StopReasons.WebConnectionFailed);
                }
            }
            finally
            {
                req.Abort();
                if (response != null)
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
                        this.statusDeletedCallback(JsonConvert.DeserializeObject<TwitterStreamDeletedEvent>(deletedstatus.ToString()));
                        return;
                    }  
                    return;
                }

                var deleteddirectmessage = delete.SelectToken("direct_message", false);
                if (deleteddirectmessage != null)
                {
                    if (this.directMessageDeletedCallback != null)
                    {
                        this.directMessageDeletedCallback(JsonConvert.DeserializeObject<TwitterStreamDeletedEvent>(deleteddirectmessage.ToString()));
                        return;
                    }
                    return;
                }
            }

            var events = obj.SelectToken("event", false);
            if (events != null)
            {
                if (this.eventCallback != null)
                {
                    this.eventCallback(JsonConvert.DeserializeObject<TwitterStreamEvent>(obj.ToString()));
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
                    this.directMessageCreatedCallback(JsonConvert.DeserializeObject<TwitterDirectMessage>(directmessage.ToString()));
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
