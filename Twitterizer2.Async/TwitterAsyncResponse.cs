//-----------------------------------------------------------------------
// <copyright file="TwitterAsyncResponse.cs" company="Patrick Ricky Smith">
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
// <summary>The response wrapper class for all asynchronous methods.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;

    /// <summary>
    /// A subclass to the <see cref="TwitterResponse{T}"/> class, for use with the asynchronous api.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TwitterAsyncResponse<T> : TwitterResponse<T>
        where T : Core.ITwitterObject
    {
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception ExceptionThrown { get; set; }
    }

    /// <summary>
    /// An extension class for converting asynchronous <see cref="TwitterResponse{T}"/> objects.
    /// </summary>
    public static class TwitterResponseAsyncConverterExtensions
    {
        /// <summary>
        /// Converts a <see cref="TwitterResponse{T}"/> to an async response.
        /// </summary>
        /// <typeparam name="T">The datatype contained in the response.</typeparam>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public static TwitterAsyncResponse<T> ToAsyncResponse<T>(this TwitterResponse<T> response)
            where T : Core.ITwitterObject
        {
            TwitterAsyncResponse<T> newResponse = new TwitterAsyncResponse<T>();
            newResponse.Content = response.Content;
            newResponse.ErrorMessage = response.ErrorMessage;
            newResponse.RateLimiting = response.RateLimiting;
            newResponse.AccessLevel = response.AccessLevel;
            newResponse.RequestUrl = response.RequestUrl;
            newResponse.ResponseObject = response.ResponseObject;
            newResponse.Result = response.Result;

            return newResponse;
        }
    }
}
