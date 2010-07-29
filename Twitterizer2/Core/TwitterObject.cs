//-----------------------------------------------------------------------
// <copyright file="TwitterObject.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The base class for all data objects.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Core
{
    using System.Xml.Serialization;
    using Twitterizer;

    public delegate void TwitterAsyncCallback<T>(T result)
            where T : TwitterObject;

    /// <summary>
    /// The base object class
    /// </summary>
    [System.Serializable]
    public class TwitterObject : ITwitterObject
    {
        public static TwitterObject Empty = new TwitterObject() { IsEmpty = true };

        /// <summary>
        /// Gets or sets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public new bool IsEmpty { get; set; }

        /// <summary>
        /// The format that all twitter dates are in.
        /// </summary>
        protected const string DateFormat = "ddd MMM dd HH:mm:ss zz00 yyyy";

        /// <summary>
        /// The format that all twitter search api dates are in.
        /// </summary>
        protected const string SearchDateFormat = "ddd, dd MMM yyyy HH:mm:ss +zz00";

        /// <summary>
        /// Gets or sets information about the user's rate usage.
        /// </summary>
        /// <value>The rate limiting object.</value>
        public RateLimiting RateLimiting { get; set; }

        /// <summary>
        /// Gets or sets the oauth tokens.
        /// </summary>
        /// <value>The oauth tokens.</value>
        [XmlIgnore, SoapIgnore]
        public OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Gets details about the request attempted.
        /// </summary>
        /// <value>The last request status.</value>
        [XmlIgnore, SoapIgnore]
        public RequestStatus RequestStatus { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(TwitterObject obj1, TwitterObject obj2)
        {
			if (object.Equals(obj1, null) || object.Equals(obj2, null))
			{
				return object.Equals(obj1, null) && object.Equals(obj2, null);
			}

            if (object.ReferenceEquals(obj1, obj2))
                return true;

            if (obj1.IsEmpty && object.ReferenceEquals(obj2, TwitterObject.Empty))
                return true;

            if (obj2.IsEmpty && object.ReferenceEquals(obj1, TwitterObject.Empty))
                return true;

            return false;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(TwitterObject obj1, TwitterObject obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
