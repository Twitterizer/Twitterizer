//-----------------------------------------------------------------------
// <copyright file="TwitterRelationship.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter relationship entity class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Twitterizer.Core;

    /// <summary>
    /// The Twitter Relationship entity class
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    [DebuggerDisplay("TwitterRelationship = {Source} -> {Target}")]
    public class TwitterRelationship : TwitterObject
    {
        /// <summary>
        /// The relationship source
        /// </summary>
        private TwitterUser source;

        /// <summary>
        /// The relationship target
        /// </summary>
        private TwitterUser target;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRelationship"/> class.
        /// </summary>
        /// <param name="tokens">OAuth access tokens.</param>
        public TwitterRelationship(OAuthTokens tokens) 
            : base()
        {
            this.Tokens = tokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterRelationship"/> class.
        /// </summary>
        internal TwitterRelationship()
            : base()
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty(PropertyName = "source")]
        public TwitterUser Source
        {
            get
            {
                if (this.source != null)
                {
                    this.source.Tokens = this.Tokens;
                }

                return this.source;
            }

            set
            {
                this.source = value;
            }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        [JsonProperty(PropertyName = "target")]
        public TwitterUser Target
        {
            get
            {
                if (this.target != null)
                {
                    this.target.Tokens = this.Tokens;
                }

                return this.target;
            }

            set
            {
                this.target = value;
            }
        }

        /// <summary>
        /// Gets or sets the relationship.
        /// </summary>
        /// <value>The relationship.</value>
        [JsonProperty(PropertyName = "relationship")]
        public TwitterRelationship Relationship
        {
            get
            {
                return this;
            }

            set
            {
                if (value != null)
                {
                    this.Target = value.Target;
                    this.Source = value.Source;
                }
            }
        }
    }
}
