//-----------------------------------------------------------------------
// <copyright file="HomeTimelineOptions.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The HomeTimelineOptions class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A payload for optional parameters of the <see cref="Twitterizer.Commands.HomeTimelineCommand"/> command.
    /// </summary>
    public sealed class HomeTimelineOptions : Core.OptionalProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeTimelineOptions"/> class.
        /// </summary>
        public HomeTimelineOptions()
            : base()
        {
            this.Page = 1;
        }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page number.</value>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the since id.
        /// </summary>
        /// <value>The since id.</value>
        [CLSCompliant(false)]
        public ulong SinceId { get; set; }

        /// <summary>
        /// Gets or sets the max id.
        /// </summary>
        /// <value>The max id.</value>
        [CLSCompliant(false)]
        public ulong MaxId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [skip user].
        /// </summary>
        /// <value><c>true</c> if [skip user]; otherwise, <c>false</c>.</value>
        public bool SkipUser { get; set; }
    }
}
