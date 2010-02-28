//-----------------------------------------------------------------------
// <copyright file="TwitterUser.cs" company="Patrick 'Ricky' Smith">
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
// <date>2010-02-26</date>
// <summary>The TwitterUser class.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Runtime.Serialization;
    using Twitterizer.OAuth;

    /// <summary>
    /// The class that represents a twitter user account
    /// </summary>
    [Serializable, DataContract(Name = "user")]
    public class TwitterUser : Core.BaseObject
    {
        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        /// <value>The User ID.</value>
        [DataMember(Name = "id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [DataMember(Name = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        /// <param name="id">The user id.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser GetUser(OAuthTokens requestTokens, long id)
        {
            Commands.UserShowCommand command = new Commands.UserShowCommand(requestTokens);
            command.UserId = id;

            command.Validate();
            if (!command.IsValid)
            {
                throw new CommandValidationException(typeof(TwitterUser), "GetUser");
            }

            return Core.Performer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="username">The username.</param>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUser"/> class.</returns>
        public static TwitterUser GetUser(OAuthTokens tokens, string username)
        {
            Commands.UserShowCommand command = new Twitterizer.Commands.UserShowCommand(tokens);
            command.Username = username;

            return Core.Performer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Gets the user's followers.
        /// </summary>
        /// <returns>A new instance of the <see cref="Twitterizer.TwitterUserCollection"/> class.</returns>
        public TwitterUserCollection GetFollowers()
        {
            Commands.UserFollowersCommand command = new Commands.UserFollowersCommand(this.Tokens);
            command.UserId = this.ID;

            TwitterUserCollection result = Core.Performer<TwitterUserCollection>.PerformAction(command);
            result.Command = command;

            return result;
        }
    }
}
