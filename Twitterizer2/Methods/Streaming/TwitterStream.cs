//-----------------------------------------------------------------------
// <copyright file="TwitterStream.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
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
namespace Twitterizer
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// The delegate to handle each status received.
    /// </summary>
    /// <param name="status">The status received.</param>
    public delegate void TwitterStatusReceivedHandler(TwitterStatus status);

    /// <summary>
    /// The TwitterStream class. Provides an interface to real-time status changes.
    /// </summary>
    public class TwitterStream : IDisposable
    {
        /// <summary>
        /// The web request
        /// </summary>
        private WebRequest request;

        /// <summary>
        /// This value is set to true to indicate that the stream connection should be closed. 
        /// </summary>
        private bool stopReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStream"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public TwitterStream(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        /// <summary>
        /// Occurs when a status is received from the stream.
        /// </summary>
        public event TwitterStatusReceivedHandler OnStatus;

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Starts the stream.
        /// </summary>
        public void StartStream()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitterizer2.EnableSSL"]) &&
                ConfigurationManager.AppSettings["Twitterizer2.EnableSSL"] == "true")
            {
                this.request = WebRequest.Create("https://stream.twitter.com/1/statuses/sample.json");
            }
            else
            {
                this.request = WebRequest.Create("http://stream.twitter.com/1/statuses/sample.json");
            }

            this.request.Credentials = new NetworkCredential(this.Username, this.Password);
            this.request.BeginGetResponse(
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
                            }

                            try
                            {
                                TwitterStatus resultObject = Core.SerializationHelper<TwitterStatus>.Deserialize(
                                    Encoding.UTF8.GetBytes(reader.ReadLine()));

                                if (this.OnStatus != null && resultObject != null && resultObject.Id > 0)
                                {
                                    this.OnStatus(resultObject);
                                }
                            }
                            catch (System.Runtime.Serialization.SerializationException)
                            {
                            }
                        }
                    }
                }, 
                this.request);
        }

        /// <summary>
        /// Ends the stream.
        /// </summary>
        public void EndStream()
        {
            this.stopReceived = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.stopReceived = true;
            this.request.Abort();
        }
    }
}
