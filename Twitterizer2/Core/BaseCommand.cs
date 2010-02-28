//-----------------------------------------------------------------------
// <copyright file="BaseCommand.cs" company="Patrick Ricky Smith">
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
// <email>ricky@digitally-born.com</email>
// <date>2010-02-25</date>
// <summary>The base class for all command classes.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using Twitterizer.OAuth;

    /// <summary>
    /// The base command class.
    /// </summary>
    /// <typeparam name="T">The business object the command should return.</typeparam>
    public abstract class BaseCommand<T> : ICommand<T>
        where T : BaseObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI for the API method.</param>
        /// <param name="tokens">The request tokens.</param>
        protected BaseCommand(string method, Uri uri, Twitterizer.OAuth.OAuthTokens tokens)
        {
            this.Uri = uri;
            this.HttpMethod = method;
            this.Tokens = tokens;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the API method URI.
        /// </summary>
        /// <value>The URI for the API method.</value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public string HttpMethod { get; set; }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public Dictionary<string, string> RequestParameters { get; set; }

        /// <summary>
        /// Gets or sets the request tokens.
        /// </summary>
        /// <value>The request tokens.</value>
        internal OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public abstract void Validate();

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <returns>The results of the command.</returns>
        /// <see cref="Twitterizer.Core.BaseObject"/>
        public T ExecuteCommand()
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            T resultObject = default(T);
            try
            {
                WebResponse webResponse = this.BuildRequest().GetResponse();

                Stream responseStream = webResponse.GetResponseStream();
                resultObject = (T)ds.ReadObject(responseStream);
                responseStream.Close();
            }
            catch (WebException wex)
            {
                throw wex.ToTwitterizerException();
            }

            resultObject.Tokens = this.Tokens;

            return resultObject;
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>A <see cref="System.Net.HttpWebRequest"/> class.</returns>
        private HttpWebRequest BuildRequest()
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            foreach (string item in this.Uri.Query.Split('&'))
            {
                queryParameters.Add(item.Split('=')[0], item.Split('=')[1]);
            }

            HttpWebRequest request = OAuthUtility.CreateOAuthRequest(
                this.Uri.AbsolutePath,
                queryParameters,
                this.HttpMethod,
                this.Tokens.ConsumerKey,
                this.Tokens.ConsumerSecret,
                this.Tokens.AccessToken,
                this.Tokens.AccessTokenSecret,
                this.Tokens.CallBackUrl);
            
            return request;
        }
    }
}
