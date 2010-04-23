//-----------------------------------------------------------------------
// <copyright file="SearchCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The search command class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer;
    using Twitterizer.Core;

    /// <summary>
    /// The create list command class
    /// </summary>
    internal sealed class SearchCommand : TwitterCommand<TwitterSearchResultWrapper>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://search.twitter.com/search.json";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCommand"/> class.
        /// </summary>
        /// <param name="requestTokens">The request tokens.</param>
        /// <param name="query">The query.</param>
        public SearchCommand(OAuthTokens requestTokens, string query)
            : base("GET", new Uri(Path), requestTokens)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query");
            }

            this.Query = query;
        }
        #endregion

        #region API Properties
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        /// <value>The locale.</value>
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the max id.
        /// </summary>
        /// <value>The max id.</value>
        public long MaxId { get; set; }

        /// <summary>
        /// Gets or sets the number per page.
        /// </summary>
        /// <value>The number per page.</value>
        public int NumberPerPage { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the since date.
        /// </summary>
        /// <value>The since date.</value>
        public DateTime SinceDate { get; set; }

        /// <summary>
        /// Gets or sets the since id.
        /// </summary>
        /// <value>The since id.</value>
        public long SinceId { get; set; }

        /// <summary>
        /// Gets or sets the geo code.
        /// </summary>
        /// <value>The geo code.</value>
        public string GeoCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to prefix the user name to the tweet.
        /// </summary>
        /// <value>
        /// <c>true</c> to prefix the user name to the tweet; otherwise, <c>false</c>.
        /// </value>
        public bool PrefixUsername { get; set; }

        /// <summary>
        /// Gets or sets the until date.
        /// </summary>
        /// <value>The until date.</value>
        public DateTime UntilDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <value>The type of the result.</value>
        public string ResultType { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            CultureInfo unitedStatesEnglishCulture = CultureInfo.GetCultureInfo("en-us");

            this.RequestParameters.Add("q", this.Query);

            if (!string.IsNullOrEmpty(this.Language))
            {
                this.RequestParameters.Add("lang", this.Language);
            }

            if (!string.IsNullOrEmpty(this.Locale))
            {
                this.RequestParameters.Add("locale", this.Locale);
            }

            if (this.MaxId > 0)
            {
                this.RequestParameters.Add("max_id", this.MaxId.ToString(unitedStatesEnglishCulture));
            }

            if (this.NumberPerPage > 0)
            {
                this.RequestParameters.Add("rpp", this.NumberPerPage.ToString(unitedStatesEnglishCulture));
            }

            if (this.PageNumber > 0)
            {
                this.RequestParameters.Add("page", this.PageNumber.ToString(unitedStatesEnglishCulture));
            }

            if (this.SinceDate > new DateTime())
            {
                this.RequestParameters.Add("since", this.SinceDate.ToString("{0:YYYY-MM-DD}", unitedStatesEnglishCulture));
            }

            if (this.SinceId > 0)
            {
                this.RequestParameters.Add("since_id", this.SinceId.ToString(unitedStatesEnglishCulture));
            }

            if (!string.IsNullOrEmpty(this.GeoCode))
            {
                this.RequestParameters.Add("geocode", this.GeoCode);
            }

            if (this.PrefixUsername)
            {
                this.RequestParameters.Add("show_user", "true");
            }

            if (this.UntilDate > new DateTime())
            {
                this.RequestParameters.Add("until", this.UntilDate.ToString("{0:YYYY-MM-DD}", unitedStatesEnglishCulture));
            }

            if (!string.IsNullOrEmpty(this.ResultType))
            {
                this.RequestParameters.Add("result_type", this.ResultType);
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = true;
        }
    }
}
