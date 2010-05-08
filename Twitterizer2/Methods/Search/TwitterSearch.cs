//-----------------------------------------------------------------------
// <copyright file="TwitterSearch.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter search class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;

    /// <summary>
    /// The Twitter Search Class
    /// </summary>
    public static class TwitterSearch
    {
        /// <summary>
        /// Searches Twitter with the the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>A <see cref="TwitterSearchResultCollection"/> instance.</returns>
        public static TwitterSearchResultCollection Search(string query)
        {
            Commands.SearchCommand command = new Twitterizer.Commands.SearchCommand(null, query);

            TwitterSearchResultCollection results =
                Core.CommandPerformer<TwitterSearchResultWrapper>.PerformAction(command).Results;

            return results;
        }

        /// <summary>
        /// Searches Twitter with the the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="sinceId">The since status id.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// A <see cref="TwitterSearchResultCollection"/> instance.
        /// </returns>
        public static TwitterSearchResultCollection Search(string query, long sinceId, int pageNumber)
        {
            Commands.SearchCommand command = new Twitterizer.Commands.SearchCommand(null, query);
            command.SinceId = sinceId;
            command.PageNumber = pageNumber;

            TwitterSearchResultCollection results =
                Core.CommandPerformer<TwitterSearchResultWrapper>.PerformAction(command).Results;

            return results;
        }

        /// <summary>
        /// Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="language">The language.</param>
        /// <param name="locale">The locale.</param>
        /// <param name="maxId">The max id.</param>
        /// <param name="numberPerPage">The number per page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="sinceDate">The since date.</param>
        /// <param name="sinceId">The since id.</param>
        /// <param name="geoCode">The geo code.</param>
        /// <param name="prefixUsername">if set to <c>true</c> [prefix username].</param>
        /// <param name="untilDate">The until date.</param>
        /// <param name="resultType">Type of the result.</param>
        /// <returns>
        /// A <see cref="TwitterSearchResultCollection"/> instance.
        /// </returns>
        public static TwitterSearchResultCollection Search(
            string query, 
            string language, 
            string locale,
            long maxId,
            int numberPerPage,
            int pageNumber,
            DateTime sinceDate,
            long sinceId,
            string geoCode,
            bool prefixUsername,
            DateTime untilDate,
            string resultType)
        {
            Commands.SearchCommand command = new Twitterizer.Commands.SearchCommand(null, query)
            {
                Language = language,
                Locale = locale,
                MaxId = maxId,
                NumberPerPage = numberPerPage,
                PageNumber = pageNumber,
                SinceDate = sinceDate,
                SinceId = sinceId,
                GeoCode = geoCode,
                PrefixUsername = prefixUsername,
                UntilDate = untilDate,
                ResultType = resultType
            };

            TwitterSearchResultCollection results =
                Core.CommandPerformer<TwitterSearchResultWrapper>.PerformAction(command).Results;

            return results;
        }
    }
}
