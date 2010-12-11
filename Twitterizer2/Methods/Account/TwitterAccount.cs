//-----------------------------------------------------------------------
// <copyright file="TwitterAccount.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
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
// <summary>The TwitterAccount class.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using Twitterizer.Core;

    /// <summary>
    /// Provides methods to request and modify details of an authorized user's account details.
    /// </summary>
    public static class TwitterAccount
    {
        /// <summary>
        /// Verifies the user's credentials.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static TwitterResponse<TwitterUser> VerifyCredentials(OAuthTokens tokens, VerifyCredentialsOptions options)
        {
            Commands.VerifyCredentialsCommand command = new Commands.VerifyCredentialsCommand(tokens, options);

            return Core.CommandPerformer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Verifies the user's credentials.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns></returns>
        public static TwitterResponse<TwitterUser> VerifyCredentials(OAuthTokens tokens)
        {
            return VerifyCredentials(tokens, null);
        }

        /// <summary>
        /// Sets one or more hex values that control the color scheme of the authenticating user's profile page on twitter.com
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// The user, with updated data, as a <see cref="TwitterUser"/>
        /// </returns>
        public static TwitterResponse<TwitterUser> UpdateProfileColors(OAuthTokens tokens, UpdateProfileColorsOptions options)
        {
            Commands.UpdateProfileColorsCommand command = new Twitterizer.Commands.UpdateProfileColorsCommand(tokens, options);

            return CommandPerformer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Updates the authenticating user's profile image. 
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="image">The avatar image for the profile. Must be a valid GIF, JPG, or PNG image of less than 700 kilobytes in size. Images with width larger than 500 pixels will be scaled down.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// The user, with updated data, as a <see cref="TwitterUser"/>
        /// </returns>
        public static TwitterResponse<TwitterUser> UpdateProfileImage(OAuthTokens tokens, TwitterImage image, OptionalProperties options)
        {
            Commands.UpdateProfileImageCommand command = new Twitterizer.Commands.UpdateProfileImageCommand(tokens, image, options);

            return CommandPerformer<TwitterUser>.PerformAction(command);
        }

        /// <summary>
        /// Updates the authenticating user's profile image.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="image">The avatar image for the profile. Must be a valid GIF, JPG, or PNG image of less than 700 kilobytes in size. Images with width larger than 500 pixels will be scaled down.</param>
        /// <returns>
        /// The user, with updated data, as a <see cref="TwitterUser"/>
        /// </returns>
        public static TwitterResponse<TwitterUser> UpdateProfileImage(OAuthTokens tokens, TwitterImage image)
        {
            return UpdateProfileImage(tokens, image, null);
        }
    }
}
