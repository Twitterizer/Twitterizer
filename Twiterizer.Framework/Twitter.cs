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
using Twitterizer.Framework.Method_Classes;
namespace Twitterizer.Framework
{
	public class Twitter
	{
		public TwitterDirectMessageMethods DirectMessages;
		public TwitterStatusMethods Status;
		public TwitterUserMethods User;
	    public TwitterSearchMethods Search;

		public Twitter(string UserName, string Password) : this(UserName, Password, "Twitterizer")
		{
		}

		public Twitter(string UserName, string Password, string Source)
		{
			DirectMessages = new TwitterDirectMessageMethods(UserName, Password);
			Status = new TwitterStatusMethods(UserName, Password, Source);
			User = new TwitterUserMethods(UserName, Password);
		    Search = new TwitterSearchMethods(UserName, Password);
		}

		public static bool VerifyCredentials(string username, string password)
		{
			TwitterRequest request = new TwitterRequest();
			TwitterRequestData data = new TwitterRequestData();
			data.UserName = username;
			data.Password = password;
			data.ActionUri = new Uri("https://twitter.com/account/verify_credentials.xml");

			try
			{
				data = request.PerformWebRequest(data, "GET");
				if (data.Users[0].ScreenName == username)
				{
					return true;
				}
			}
			catch { } // ignore exeptions - authentication failed

			return false;
		}
	}
}
