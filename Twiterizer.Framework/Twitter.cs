/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2008, Patrick "Ricky" Smith <ricky@digitally-born.com>
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
using System;
namespace Twitterizer.Framework
{
	public class Twitter
	{
		public TwitterDirectMessageMethods DirectMessages;
		public TwitterStatusMethods Status;
		public TwitterUserMethods User;

        public string ProxyUri { get; set; }

        public const string DefaultSource = "Twitterizer";

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
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
		public Twitter(string UserName, string Password) :
            this(UserName, Password, DefaultSource)
		{
			
		}        

        /// <summary>
        /// Initializes a new instance of the <see cref="Twitter"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Source">The source.</param>
		public Twitter(string UserName, string Password, string Source)
		{
			DirectMessages = new TwitterDirectMessageMethods(UserName, Password);
            User = new TwitterUserMethods(UserName, Password);
            Status = new TwitterStatusMethods(UserName, Password, Source);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Twitter"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Source">The source.</param>
        /// <param name="ProxyURI">If you have proxy set this variable URI</param>
        public Twitter(string UserName, string Password, string Source, string ProxyURI)
        {
            DirectMessages = new TwitterDirectMessageMethods(UserName, Password, ProxyURI);
            Status = new TwitterStatusMethods(UserName, Password, Source, ProxyURI);
            User = new TwitterUserMethods(UserName, Password, ProxyURI);
        }

        /// <summary>
        /// Verifies the given credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="ProxyUri">The proxy URI.</param>
        /// <returns></returns>
		public static bool VerifyCredentials(string username, string password, string ProxyUri)
		{
            TwitterRequest request = new TwitterRequest(ProxyUri);
			TwitterRequestData data = new TwitterRequestData();            
			data.UserName = username;
			data.Password = password;
			data.ActionUri = new Uri("https://twitter.com/account/verify_credentials.xml");

			try
			{
				data = request.PerformWebRequest(data, "GET");
                return (data != null && data.Users != null && data.Users.Count > 0 && data.Users[0].ScreenName.ToLower() == username.ToLower());
			}
			catch(Exception ex) { } // ignore exeptions - authentication failed

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
