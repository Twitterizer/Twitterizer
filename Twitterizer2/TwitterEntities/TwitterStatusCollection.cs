//-----------------------------------------------------------------------
// <copyright file="TwitterStatusCollection.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The collection of TwitterStatus objects.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using Twitterizer.Core;

    /// <summary>
    /// The TwitterStatusCollection class.
    /// </summary>
    public class TwitterStatusCollection : BaseCollection<TwitterStatus>
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        [IgnoreDataMember]
        internal PagedCommand<TwitterStatusCollection> Command { get; set; }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public TwitterStatusCollection GetNextPage()
        {
            if (this.Command == null || this.Command.Page <= 1)
            {
                return null;
            }

            PagedCommand<TwitterStatusCollection> newCommand = this.Command.Clone();
            newCommand.Page += 1;

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(newCommand);
            result.Command = newCommand;
            return result;
        }

        /// <summary>
        /// Gets the previous page.
        /// </summary>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public TwitterStatusCollection GetPreviousPage()
        {
            if (this.Command == null || this.Command.Page <= 1)
            {
                return null;
            }

            PagedCommand<TwitterStatusCollection> newCommand = this.Command.Clone();
            newCommand.Page -= 1;

            if (newCommand.Page <= 0)
            {
                return null;
            }

            TwitterStatusCollection result = Core.CommandPerformer<TwitterStatusCollection>.PerformAction(newCommand);
            result.Command = newCommand;
            return result;
        }
    }
}
