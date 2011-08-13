//-----------------------------------------------------------------------
// <copyright file="TwitterMediaEntity.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net)
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
// <summary>The twitter url entity class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Entities
{
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Represents a pre-parsed media entity located within the body of a <see cref="Twitterizer.TwitterStatus.Text"/>.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class TwitterMediaEntity : TwitterUrlEntity
    {
        public enum MediaTypes
        {
            Unknown,
            Photo
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterMediaEntity"/> class.
        /// </summary>
        internal TwitterMediaEntity() { }

        public MediaTypes MediaType { get; set; }
        public decimal Id { get; set; }
        public string IdString { get; set; }
        public string MediaUrl { get; set; }
        public string MediaUrlSecure { get; set; }
        //public string Url { get; set; }
        //public string DisplayUrl { get; set; }
        //public string ExpandedUrl { get; set; }
        public List<MediaSize> Sizes { get; set; }
        
        public class MediaSize
        {
            public enum MediaSizeResizes
            {
                Unknown,
                Crop,
                Fit
            }

            public enum MediaSizes
            {
                Unknown,
                Thumb,
                Small,
                Medium,
                Large
            }

            public MediaSizes Size { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public MediaSizeResizes Resize { get; set; }
        }
    }
}
