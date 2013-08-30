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
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Twitterizer.Core;

    /// <summary>
    /// The different stop reasons for stopping a stream.
    /// </summary>
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
    public delegate void RawJsonCallback(string json);


    /// <summary>
    ///   The TwitterStream class. Provides an interface to real-time status changes.
    /// </summary>
    public class TwitterStream : IDisposable
    {
        private DirectMessageCreatedCallback directMessageCreatedCallback;
        private DirectMessageDeletedCallback directMessageDeletedCallback;
        private EventCallback eventCallback;
        private InitUserStreamCallback friendsCallback;
        private RawJsonCallback rawJsonCallback;
        private StatusCreatedCallback statusCreatedCallback;
        private StatusDeletedCallback statusDeletedCallback;

        /// <summary>
        ///   The userAgent which shall be used in connections to Twitter (a must in the specs of the API)
        /// </summary>
        private string userAgent = null;

        /// <summary>
        ///   This value is set to true to indicate that the stream connection should be closed.
        /// </summary>
        private bool stopReceived;

        private HttpWebRequest request;

        private StreamStoppedCallback streamStoppedCallback;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "TwitterStream" /> class.
        /// </summary>
        /// <param name = "tokens">The tokens.</param>
        /// <param name = "userAgent">The user agent string which shall include the version of your client.</param>
        /// <param name = "streamoptions">The stream or user stream options to intially use when starting the stream.</param>
        public TwitterStream(OAuthTokens tokens, string userAgent, StreamOptions streamoptions)
        {
#if !SILVERLIGHT
            // No non-silverlight user-agent as Assembly.GetName() isn't supported and setting the request.UserAgent is also not supported.
            if (string.IsNullOrEmpty(userAgent))
            {
                this.userAgent = string.Format(
                    CultureInfo.InvariantCulture,
                    "Twitterizer/{0}",
                    Assembly.GetExecutingAssembly().GetName().Version);
            }
            else
            {
                this.userAgent = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0} (via Twitterizer/{1})",
                    userAgent,
                    Assembly.GetExecutingAssembly().GetName().Version);
            }
#endif
            Tokens = tokens;

            if (streamoptions != null)
                StreamOptions = streamoptions;
        }

        /// <summary>
        ///   Gets or sets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        public OAuthTokens Tokens { get; set; }

        /// <summary>
        ///   Gets or sets the stream options.
        /// </summary>
        /// <value>The stream options.</value>
        public StreamOptions StreamOptions { get; set; }

        /// <summary>
        ///   Gets or sets the Basic Auth Credentials.
        /// </summary>
        /// <value>The Basic Auth Credentials.</value>
        public NetworkCredential NetworkCredentials { get; set; }

        #region IDisposable Members

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            EndStream();
            friendsCallback = null;
            streamStoppedCallback = null;
            statusCreatedCallback = null;
            statusDeletedCallback = null;
            directMessageCreatedCallback = null;
            directMessageDeletedCallback = null;
            eventCallback = null;
            rawJsonCallback = null;
        }

        #endregion

        /// <summary>
        ///   Starts the user stream.
        /// </summary>
        public IAsyncResult StartUserStream(
            InitUserStreamCallback friendsCallback,
            StreamStoppedCallback streamStoppedCallback,
            StatusCreatedCallback statusCreatedCallback,
            StatusDeletedCallback statusDeletedCallback,
            DirectMessageCreatedCallback directMessageCreatedCallback,
            DirectMessageDeletedCallback directMessageDeletedCallback,
            EventCallback eventCallback,
            RawJsonCallback rawJsonCallback = null
            )
        {
            if (request != null)
            {
                throw new InvalidOperationException("Stream is already open");
            }

            WebRequestBuilder builder = new WebRequestBuilder(new Uri("https://userstream.twitter.com/2/user.json"),
                                                              HTTPVerb.POST, Tokens, userAgent);

            PrepareStreamOptions(builder);

            if (StreamOptions != null && StreamOptions is UserStreamOptions)
            {
                if ((StreamOptions as UserStreamOptions).AllReplies)
                    builder.Parameters.Add("replies", "all");
            }

            request = builder.PrepareRequest();
            this.friendsCallback = friendsCallback;
            this.streamStoppedCallback = streamStoppedCallback;
            this.statusCreatedCallback = statusCreatedCallback;
            this.statusDeletedCallback = statusDeletedCallback;
            this.directMessageCreatedCallback = directMessageCreatedCallback;
            this.directMessageDeletedCallback = directMessageDeletedCallback;
            this.eventCallback = eventCallback;
            this.rawJsonCallback = rawJsonCallback;
            stopReceived = false;
#if SILVERLIGHT
            request.AllowReadStreamBuffering = false;
#else
            request.Timeout = 10000;
#endif
            return request.BeginGetResponse(StreamCallback, request);
        }


        /// <summary>
        ///   Starts the public stream.
        /// </summary>
        public IAsyncResult StartPublicStream(
            StreamStoppedCallback streamStoppedCallback,
            StatusCreatedCallback statusCreatedCallback,
            StatusDeletedCallback statusDeletedCallback,
            EventCallback eventCallback,
            RawJsonCallback rawJsonCallback = null
            )
        {
            if (request != null)
            {
                throw new InvalidOperationException("Stream is already open");
            }

            WebRequestBuilder builder;
            if (Tokens == null)
                builder = new WebRequestBuilder(new Uri("https://stream.twitter.com/1/statuses/filter.json"),
                                                HTTPVerb.POST, userAgent, NetworkCredentials);
            else
                builder = new WebRequestBuilder(new Uri("https://stream.twitter.com/1/statuses/filter.json"),
                                                HTTPVerb.POST, Tokens, userAgent);
            PrepareStreamOptions(builder);

            request = builder.PrepareRequest();

            this.streamStoppedCallback = streamStoppedCallback;
            this.statusCreatedCallback = statusCreatedCallback;
            this.statusDeletedCallback = statusDeletedCallback;
            this.eventCallback = eventCallback;
            this.rawJsonCallback = rawJsonCallback;
            stopReceived = false;
#if SILVERLIGHT
            request.AllowReadStreamBuffering = false;
#endif
            return request.BeginGetResponse(StreamCallback, request);
        }

        /// <summary>
        ///   Prepares the stream options.
        /// </summary>
        /// <param name = "builder">The builder.</param>
        private void PrepareStreamOptions(WebRequestBuilder builder)
        {
            if (StreamOptions != null)
            {
                if (StreamOptions.Count > 0)
                    builder.Parameters.Add("count", StreamOptions.Count.ToString());

                if (StreamOptions.Follow != null && StreamOptions.Follow.Count > 0)
                    builder.Parameters.Add("follow", string.Join(",", StreamOptions.Follow.ToArray()));

                if (StreamOptions.Locations != null && StreamOptions.Locations.Count > 0)
                    builder.Parameters.Add("locations",
                                           string.Join(",",
                                                       StreamOptions.Locations.Select((x, r) => x.ToString()).ToArray()));

                if (StreamOptions.Track != null && StreamOptions.Track.Count > 0)
                    builder.Parameters.Add("track", string.Join(",", StreamOptions.Track.ToArray()));

                builder.UseCompression = StreamOptions.UseCompression;
            }
        }

        /// <summary>
        ///   The callback handler for all streams
        /// </summary>
        /// <param name = "result">The result.</param>
        private void StreamCallback(IAsyncResult result)
        {
            HttpWebRequest req = (HttpWebRequest)result.AsyncState;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)req.EndGetResponse(result);

                if (response.StatusCode == HttpStatusCode.OK)
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

                            while (!stopReceived && !reader.EndOfStream)
                            {
                                string lineOfData = reader.ReadLine();

                                if (stopReceived || lineOfData == null)
                                {
                                    break;
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
                                        var blockbuilderstring = blockBuilder.ToString();

                                        if (rawJsonCallback != null)
                                        {
                                            rawJsonCallback(blockbuilderstring);
                                        }
                                        ThreadPool.QueueUserWorkItem(delegate { ParseMessage(blockbuilderstring.Trim()); });
                                        blockBuilder.Clear();
                                    }
                                }
                            }

                            reader.Close();
                            OnStreamStopped(stopReceived ? StopReasons.StoppedByRequest : StopReasons.WebConnectionFailed);
                        }
                        catch
                        {
                            reader.Close();
                            OnStreamStopped(stopReceived ? StopReasons.StoppedByRequest : StopReasons.WebConnectionFailed);
                        }
                    }
                }
            }
            catch (WebException we)
            {
                HttpWebResponse httpResponse = we.Response as HttpWebResponse;
                if (httpResponse != null)
                {
                    switch ((httpResponse).StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            {
                                OnStreamStopped(StopReasons.Unauthorised);
                                break;
                            }
                        case HttpStatusCode.Forbidden:
                            {
                                OnStreamStopped(StopReasons.Forbidden);
                                break;
                            }
                        case HttpStatusCode.NotFound:
                            {
                                OnStreamStopped(StopReasons.NotFound);
                                break;
                            }
                        case HttpStatusCode.NotAcceptable:
                            {
                                OnStreamStopped(StopReasons.NotAcceptable);
                                break;
                            }
                        case HttpStatusCode.RequestEntityTooLarge:
                            {
                                OnStreamStopped(StopReasons.TooLong);
                                break;
                            }
                        case HttpStatusCode.RequestedRangeNotSatisfiable:
                            {
                                OnStreamStopped(StopReasons.RangeUnacceptable);
                                break;
                            }
                        case (HttpStatusCode)420: //Rate Limited
                            {
                                OnStreamStopped(StopReasons.RateLimited);
                                break;
                            }
                        case HttpStatusCode.InternalServerError:
                            {
                                OnStreamStopped(StopReasons.TwitterServerError);
                                break;
                            }
                        case HttpStatusCode.ServiceUnavailable:
                            {
                                OnStreamStopped(StopReasons.TwitterOverloaded);
                                break;
                            }
                        default:
                            {
                                OnStreamStopped(StopReasons.Unknown);
                                break;
                            }
                    }
                }
                else
                {
                    OnStreamStopped(StopReasons.Unknown);
                }
            }
            catch (Exception)
            {
                OnStreamStopped(StopReasons.WebConnectionFailed);
            }
            finally
            {
                req.Abort();
                if (response != null)
                    response.Close();
                request = null;
            }
        }

        private static string ConvertJTokenToString(JToken token)
        {
            if (token != null)
                return token.ToString().Trim();
            return null;
        }

        /// <summary>
        ///   Parses the message.
        /// </summary>
        /// <param name = "p">The p.</param>
        private void ParseMessage(string p)
        {
            JObject obj = (JObject)JsonConvert.DeserializeObject(p);

            var friends = obj.SelectToken("friends", false);
            if (friends != null)
            {
                if (friendsCallback != null && friends.HasValues)
                {
                    friendsCallback(JsonConvert.DeserializeObject<TwitterIdCollection>(ConvertJTokenToString(friends)));
                }
                return;
            }

            var delete = obj.SelectToken("delete", false);
            if (delete != null)
            {
                var deletedStatus = delete.SelectToken("status", false);
                if (deletedStatus != null)
                {
                    if (statusDeletedCallback != null && deletedStatus.HasValues)
                    {
                        statusDeletedCallback(JsonConvert.DeserializeObject<TwitterStreamDeletedEvent>(ConvertJTokenToString(deletedStatus)));
                    }
                    return;
                }

                var deletedDirectMessage = delete.SelectToken("direct_message", false);
                if (deletedDirectMessage != null)
                {
                    if (directMessageDeletedCallback != null && deletedDirectMessage.HasValues)
                    {
                        directMessageDeletedCallback(JsonConvert.DeserializeObject<TwitterStreamDeletedEvent>(ConvertJTokenToString(deletedDirectMessage)));
                    }
                    return;
                }
            }

            var events = obj.SelectToken("event", false);
            if (events != null)
            {
                if (eventCallback != null)
                {
                    var targetobject = obj.SelectToken("target_object", false);
                    TwitterObject endtargetobject = null;
                    if (targetobject != null)
                    {
                        if (targetobject.SelectToken("subscriber_count", false) != null)
                        {
                            endtargetobject = JsonConvert.DeserializeObject<TwitterList>(targetobject.ToString());
                        }
                        else if (targetobject.SelectToken("user", false) != null)
                        {
                            endtargetobject = JsonConvert.DeserializeObject<TwitterStatus>(targetobject.ToString());
                        }
                    }
                    var endevent = JsonConvert.DeserializeObject<TwitterStreamEvent>(obj.ToString());
                    endevent.TargetObject = endtargetobject;
                    this.eventCallback(endevent);
                }
                return;
            }

            var user = obj.SelectToken("user", false);
            if (user != null)
            {
                if (statusCreatedCallback != null && user.HasValues)
                {
                    statusCreatedCallback(JsonConvert.DeserializeObject<TwitterStatus>(ConvertJTokenToString(obj)));
                }
                return;
            }

            var directMessage = obj.SelectToken("direct_message", false);
            if (directMessage != null)
            {
                if (directMessageCreatedCallback != null && directMessage.HasValues)
                {
                    directMessageCreatedCallback(JsonConvert.DeserializeObject<TwitterDirectMessage>(ConvertJTokenToString(directMessage)));
                }
            }
        }

        /// <summary>
        ///   Called when the stream is stopped.
        /// </summary>
        /// <param name = "reason">The reason.</param>
        /// <remarks>
        /// </remarks>
        private void OnStreamStopped(StopReasons reason)
        {
            if (streamStoppedCallback != null)
                streamStoppedCallback(reason);
        }

        /// <summary>
        /// Ends the stream.
        /// </summary>
        public void EndStream()
        {
            stopReceived = true;
            if (request != null)
            {
                request.Abort();
                request = null;
            }
        }
    }
}