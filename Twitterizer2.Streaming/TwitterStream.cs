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
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The delegates to handle each status received.
    /// </summary>
    /// <param name="status">The status received.</param>
    public delegate void TwitterStatusReceivedHandler(TwitterStatus status);

    public delegate void TwitterStatusDeletedHandler(TwitterStatus status);

    /// <summary>
    /// The delegate to handle changes in the friend list
    /// </summary>
    /// <param name="friendList">List of user ids</param>
    public delegate void TwitterFriendsReceivedHandler(List<decimal> friendList);

    public enum StopReasons
    {
        StoppedByRequest,
        WebConnectionFailed,
        Unknown
    }
    /// <summary>
    /// The delegate telling that the streaming interface stopped out of any reason
    /// </summary>
    /// <param name="reason">The reason why the streaming stopped (if available)</param>
    public delegate void StreamingInterfaceStopped(StopReasons reason, string description);

    /// <summary>
    /// This delegate gives the raw received status to the using app when we don't know how to handle it
    /// So the app can maybe implement its own parser for that until we implement it here or
    /// know at least there was something, give us the message and we can see
    /// </summary>
    /// <param name="message"></param>
    public delegate void UnknownMessageTypeReceived(string message);


    /// <summary>
    /// The TwitterStream class. Provides an interface to real-time status changes.
    /// </summary>
    public class TwitterStream : IDisposable
    {
        /// <summary>
        /// This value is set to true to indicate that the stream connection should be closed. 
        /// </summary>
        private bool stopReceived;

        /// <summary>
        /// The useragant which shall be used in connections to Twitter (a must in the specs of the API)
        /// </summary>
        private string userAgent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStream"/> class.
        /// </summary>
        /// <param name="tokes">The OAuth tokens.</param>
        /// <param name="userAgent">The useragent string which shall include the version of your client.</param>
        public TwitterStream(OAuthTokens tokens, string UserAgent)
        {
            userAgent = UserAgent;
            this.Tokens = tokens;
        }

        public OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Occurs when a status is received from the stream.
        /// </summary>
        public event TwitterStatusReceivedHandler OnStatusReceived;

        public event TwitterStatusDeletedHandler OnStatusDeleted;

        public event TwitterFriendsReceivedHandler OnFriendsReceived;

        /// <summary>
        /// Events which shall give the using app the possibility to react on stream changes
        /// </summary>
        public event StreamingInterfaceStopped OnStreamingStopped;

        /// <summary>
        /// Event if we get an unkonwn message type
        /// </summary>
        public event UnknownMessageTypeReceived OnUnknownMessageTypeReceived;

        /// <summary>
        /// Starts the filter stream. Returns public statuses that match one or more filter predicates.
        /// </summary>
        /// <param name="options">The options.</param>
        public void StartFilterStream(FilterStreamOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            StringBuilder uriBuilder = new StringBuilder();
            if (options.UseSSL)
                uriBuilder.Append("https://");
            else
                uriBuilder.Append("http://");

            uriBuilder.Append("stream.twitter.com/1/statuses/filter.json?");

            if (options.Count != 0)
            {
                uriBuilder.AppendFormat("count={0}&", options.Count);
            }

            if (options.Follow != null && options.Follow.Count > 0)
            {
                uriBuilder.AppendFormat("follow={0}&", string.Join(",", options.Follow.ToArray()));
            }

            if (options.Track != null && options.Track.Count > 0)
            {
                uriBuilder.AppendFormat("track={0}&", string.Join(",", options.Track.ToArray()));
            }

            if (options.Locations != null && options.Locations.Count > 0)
            {
                uriBuilder.Append("locations=");

                foreach (Location location in options.Locations)
                {
                    uriBuilder.AppendFormat("{0},", location.ToString());
                }

                uriBuilder = uriBuilder.Remove(uriBuilder.Length - 1, 1);
            }

            StartStream(uriBuilder.ToString());
        }

        /// <summary>
        /// Starts the user stream.
        /// </summary>
        public void StartUserStream()
        {
            WebRequestBuilder builder = new WebRequestBuilder(new Uri("https://userstream.twitter.com/2/user.json"), HTTPVerb.GET, this.Tokens);

            HttpWebRequest request = builder.PrepareRequest();
            request.KeepAlive = true;
            request.UserAgent = userAgent;

            request.BeginGetResponse(StreamCallback, request);
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
                // taking care of lines may contain \n in text - \r is garanteed being always last line
                string olderLines = "";
                try
                {
                    while (!this.stopReceived && !reader.EndOfStream)
                    {
                        string lineOfData = reader.ReadLine();
                        if (!lineOfData.EndsWith("\r"))
                        {
                            olderLines += lineOfData;
                            continue;
                        }
                        else
                        {
                            lineOfData = olderLines + lineOfData;
                            olderLines = "";
                        }

                        if (string.IsNullOrWhiteSpace(lineOfData))
                            continue;

                        if (ReadStatus(lineOfData))
                            continue;

                        if (ReadStatusDeleted(lineOfData))
                            continue;

                        if (ReadFollowers(lineOfData))
                            continue;

                        OnUnknownMessageTypeReceived(lineOfData);
                    }
                }
                catch (Exception exp)
                {
                    // maybe also an event!!?
                }
                if (this.stopReceived)
                {
                    OnStreamingStopped(StopReasons.StoppedByRequest, "Stopped by request");
                } 
                else if (reader.EndOfStream)
                {
                    OnStreamingStopped(StopReasons.WebConnectionFailed, "Reader end of stream");
                }
                
                req.Abort();
                reader.Close();
                response.Close();
            }
        }

        private bool ReadFollowers(string lineOfData)
        {
            if (string.IsNullOrWhiteSpace(lineOfData))
                return false;

            try
            {
                JObject deserializedObject = (JObject)JsonConvert.DeserializeObject(lineOfData);

                if (deserializedObject == null || deserializedObject.SelectToken("friends") == null)
                    return false;

                List<decimal> resultList = JsonConvert.DeserializeObject<List<decimal>>(deserializedObject.SelectToken("friends").ToString());

                if (resultList != null && this.OnFriendsReceived != null)
                {
                    OnFriendsReceived(resultList);
                }
            }
            catch (JsonSerializationException)
            {
                return false;
            }

            return true;
        }

        private bool ReadStatus(string lineOfData)
        {
            try
            {
                TwitterStatus resultStatus = Twitterizer.Core.SerializationHelper<TwitterStatus>.Deserialize(
                    Encoding.UTF8.GetBytes(lineOfData));

                if (resultStatus == null || resultStatus.Id <= 0)
                {
                    return false;
                }

                if (this.OnStatusReceived != null)
                {
                    this.OnStatusReceived(resultStatus);
                }
            }
            catch (SerializationException)
            {
                return false;
            }

            return true;
        }

        private bool ReadStatusDeleted(string lineOfData)
        {
            try
            {
                JObject deserializedObject = (JObject)JsonConvert.DeserializeObject(lineOfData);

                if (deserializedObject == null || deserializedObject.SelectToken("delete") == null || deserializedObject.SelectToken("delete").SelectToken("status") == null)
                    return false;

                TwitterStatus resultStatus = JsonConvert.DeserializeObject<TwitterStatus>(deserializedObject.SelectToken("delete").SelectToken("status").ToString());

                if (resultStatus != null && resultStatus.Id > 0 && this.OnStatusDeleted != null)
                {
                    this.OnStatusDeleted(resultStatus);
                }
            }
            catch (SerializationException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Starts the stream.
        /// </summary>
        private void StartStream(string streamUri)
        {
            WebRequestBuilder builder = new WebRequestBuilder(new Uri(streamUri), HTTPVerb.GET, this.Tokens);

            HttpWebRequest request;
            request = builder.PrepareRequest();
            request.UserAgent = userAgent;

            request.BeginGetResponse(
                ar =>
                {
                    var req = (WebRequest)ar.AsyncState;

                    // TODO: Add exception handling: EndGetResponse could throw
                    using (var response = req.EndGetResponse(ar))
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        // This loop goes as long as twitter is streaming
                        while (!reader.EndOfStream)
                        {
                            // If the calling application has asked to halt the connection, this will abort the connection.
                            if (this.stopReceived)
                            {
                                req.Abort();
                                OnStreamingStopped(StopReasons.StoppedByRequest, "Stop request received");
                                return;
                            }

                            try
                            {
                                TwitterStatus resultObject = Twitterizer.Core.SerializationHelper<TwitterStatus>.Deserialize(
                                    Encoding.UTF8.GetBytes(reader.ReadLine()));


                                if (resultObject != null && resultObject.Id > 0 && this.OnStatusReceived != null)
                                {
                                    this.OnStatusReceived(resultObject);
                                }
                            }
                            catch (System.Runtime.Serialization.SerializationException)
                            {
                            }
                        }
                    }
                },
                request);
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
            OnStreamingStopped(reason, description);
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
