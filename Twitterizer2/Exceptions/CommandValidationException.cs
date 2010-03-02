//-----------------------------------------------------------------------
// <copyright file="CommandValidationException.cs" company="Patrick 'Ricky' Smith">
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
// <summary>Provides a means of reporting command validation failures.</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using Twitterizer.Core;

    /// <summary>
    /// An exception class indicating that required parameters were missing from a command.
    /// </summary>
    public class CommandValidationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidationException"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public CommandValidationException(Type sender)
            : base(string.Format("The command ({0}) failed validation.", sender.Name))
        {
            this.Sender = sender;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandValidationException"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="methodName">Name of the method.</param>
        public CommandValidationException(Type sender, string methodName)
            : base(string.Format("The command ({0}) failed validation.", sender.Name))
        {
            this.Sender = sender;
            this.MethodName = methodName;
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public Type Sender { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }
    }
}
