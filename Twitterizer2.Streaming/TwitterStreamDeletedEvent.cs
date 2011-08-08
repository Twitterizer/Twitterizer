﻿//-----------------------------------------------------------------------
// <copyright file="TwitterStatus.cs" company="Patrick 'Ricky' Smith">
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
// <author>David Golden</author>
// <summary>The TwitterStreamDeletedEvent class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Streaming
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    public class TwitterStreamDeletedEvent
    {

        /// <summary>
        /// Gets or sets the user id of the event. This is always the user who initiated the event.
        /// </summary>
        /// <value>The User Id.</value>
#if !SILVERLIGHT        
        [DataMember]
#endif
        [JsonProperty(PropertyName = "user_id")]
        public decimal UserId { get; set; }

        /// <summary>
        /// Gets or sets the id of the event. This is the status that was affected.
        /// </summary>
        /// <value>The Direct Message or Status Id.</value>
#if !SILVERLIGHT        
        [DataMember]
#endif
        [JsonProperty(PropertyName = "id")]
        public decimal Id { get; set; }
    }
}
