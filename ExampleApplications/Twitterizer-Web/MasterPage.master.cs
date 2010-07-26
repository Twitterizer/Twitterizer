//-----------------------------------------------------------------------
// <copyright file="MasterPage.master.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The example web application master page.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.ExampleWeb
{
    using System;
    using System.Configuration;
    using Twitterizer;

    public partial class MasterPage : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        public OAuthTokens Tokens
        {
            get
            {
                OAuthTokens tokens = Session["OAuthTokens"] as OAuthTokens;

                if (tokens == null)
                {
                    Response.Redirect("~/authenticate/", true);
                }

                return tokens;
            }
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public decimal UserId
        {
            get
            {
                if (Session["UserId"] != null)
                    return (decimal)Session["UserId"];

                return 0;
            }
        }

        /// <summary>
        /// Gets the user's screen name
        /// </summary>
        /// <value>The screen name.</value>
        public string ScreenName
        {
            get
            {
                if (!string.IsNullOrEmpty(Session["Username"] as string))
                    return (string)Session["Username"];

                return string.Empty;
            }
        }

        public string LinkifyText(string text)
        {
            string pathToUserPage = string.Format("{0}/user.aspx", Request.Path);

            text = System.Text.RegularExpressions.Regex.Replace(text, @"@([^ ]+)", string.Format(@"@<a href=""{0}?username=$1"" ref=""nofollow"" target=""_blank"">$1</a>", pathToUserPage));
            text = System.Text.RegularExpressions.Regex.Replace(text, @"(?<addr>http://[^ ]+|www\.[^ ]+)", @"<a href=""${addr}"" ref=""nofollow"" target=""_blank"">$1</a>");

            return text;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OAuthTokens"] != null)
            {
                TwitterRateLimitStatus status = TwitterRateLimitStatus.GetStatus(this.Tokens);
                this.RemainingRequestsLabel.Text = string.Format("{0} requests remaining.", status.RemainingHits);
            }
        }
    }
}