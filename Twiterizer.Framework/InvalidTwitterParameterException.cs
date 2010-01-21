/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2010, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */

namespace Twitterizer.Framework
{
    using System;

    /// <summary>
    /// Describes the reason a TwitterParameter cannot be used.
    /// </summary>
    public enum InvalidTwitterParameterReason
    {
        /// <summary>
        /// A required parameter was not supplied.
        /// </summary>
        MissingRequiredParameter = 0,
        
        /// <summary>
        /// The parameter value was invalid.
        /// </summary>
        ValueOutOfRange = 1,

        /// <summary>
        /// The parameter is not supported for the method invoked.
        /// </summary>
        ParameterNotSupported = 2
    }

    /// <summary>
    /// The InvalidTwitterParameterException class.
    /// </summary>
    public class InvalidTwitterParameterException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTwitterParameterException"/> class.
        /// </summary>
        /// <param name="Parameter">The parameter.</param>
        /// <param name="Reason">The reason.</param>
        public InvalidTwitterParameterException(TwitterParameterNames parameter, InvalidTwitterParameterReason reason)
        {
            this.Parameter = parameter;
            this.Reason = reason;
        }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public TwitterParameterNames Parameter { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public InvalidTwitterParameterReason Reason { get; set; }
    }
}
