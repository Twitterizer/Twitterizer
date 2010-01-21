/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2010, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */
namespace Twitterizer.Framework
{
    using System;
    using System.Configuration;

    public class Twitter
    {
        /// <summary>
        /// The default source sent to twitter for all status updates.
        /// </summary>
        public const string DefaultSource = "Twitterizer";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Twitter"/> class.
        /// </summary>
        public Twitter()
            : this(string.Empty, string.Empty, DefaultSource)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Twitter"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public Twitter(string userName, string password) :
            this(userName, password, DefaultSource)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Twitter"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="source">The source.</param>
        public Twitter(string userName, string password, string source)
        {
            this.DirectMessages = new TwitterDirectMessageMethods(userName, password);
            this.User = new TwitterUserMethods(userName, password);
            this.Status = new TwitterStatusMethods(userName, password, source);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Twitter"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="source">The source.</param>
        /// <param name="proxyURI">The proxy URI.</param>
        public Twitter(string userName, string password, string source, string proxyURI)
        {
            this.DirectMessages = new TwitterDirectMessageMethods(userName, password, proxyURI);
            this.Status = new TwitterStatusMethods(userName, password, source, proxyURI);
            this.User = new TwitterUserMethods(userName, password, proxyURI);
        }
        #endregion

        /// <summary>
        /// Gets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public static string Domain
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Twitterizer.Domain"]))
                {
                    return ConfigurationManager.AppSettings["Twitterizer.Domain"];
                }
                else
                {
                    return "http://api.twitter.com/1/";
                }
            }
        }

        /// <summary>
        /// Gets or sets the direct messages.
        /// </summary>
        /// <value>The direct messages.</value>
        public TwitterDirectMessageMethods DirectMessages { get; private set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public TwitterStatusMethods Status { get; private set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public TwitterUserMethods User { get; private set; }

        /// <summary>
        /// Gets or sets the proxy URI.
        /// </summary>
        /// <value>The proxy URI.</value>
        public string ProxyUri { get; set; }

        /// <summary>
        /// Verifies the given credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="ProxyUri">The proxy URI.</param>
        /// <returns></returns>
        public static bool VerifyCredentials(string username, string password, string proxyUri)
        {
            TwitterRequest request = new TwitterRequest(proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = username;
            data.Password = password;
            data.ActionUri = new Uri("https://twitter.com/account/verify_credentials.xml");

            try
            {
                data = request.PerformWebRequest(data, "GET");
                return data != null && data.Users != null && data.Users.Count > 0 && data.Users[0].ScreenName.ToLower() == username.ToLower();
            }
            catch (Exception) 
            {
                // ignore exeptions - authentication failed
            } 

            return false;
        }

        /// <summary>
        /// Verifies the given credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static bool VerifyCredentials(string username, string password)
        {
            return VerifyCredentials(username, password, string.Empty);
        }
    }
}