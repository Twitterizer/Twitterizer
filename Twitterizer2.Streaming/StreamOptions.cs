//-----------------------------------------------------------------------
// <copyright file="StreamOptions.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The Stream Options parameters class.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Streaming
{
    using System.Collections.Generic;

    public class StreamOptions
    {
        public StreamOptions()
        {
            this.Track = new List<string>();
            this.Locations = new List<Location>();
            this.Follow = new List<string>();
        }

        /// <summary>
        /// Gets or sets the number of previous statuses to consider for delivery before transitioning to live stream delivery.
        /// </summary>
        /// <value>The count.</value>
        /// <remarks>Currently disabled by Twitter. On unfiltered streams, all considered statuses are delivered, so the number requested is the number returned. On filtered streams, the number requested is the number of statuses that are applied to the filter predicate, and not the number of statuses returned.</remarks>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the user IDs that is to be referenced in the stream.
        /// </summary>
        /// <value>The follow.</value>
        public List<string> Follow { get; set; }

        /// <summary>
        /// Gets or sets the keywords to track.
        /// </summary>
        /// <value>The keywords to track.</value>
        public List<string> Track { get; set; }

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>The locations.</value>
        public List<Location> Locations { get; set; }

        /// <summary>
        /// Gets or sets whether to request the use of GZip compression on the stream.
        /// </summary>
        /// <value>Boolean.</value>
        /// <remarks>Will use the recently introduced GZip compression to decrease bandwitdth.</remarks>
        public bool UseCompression { get; set; }
        
        #if !SILVERLIGHT        
        /// <summary>
        /// Gets or sets the proxy.
        /// </summary>
        /// <value>
        /// The proxy.
        /// </value>
        public System.Net.WebProxy Proxy { get; set; }
#endif
    }
}