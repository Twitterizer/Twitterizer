//-----------------------------------------------------------------------
// <copyright file="CopyToDataTableExtensions.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The Copy To Data Table Extensions class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer.Data
{
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// The CopyToDataTableExtensions class.
    /// </summary>
    public static class CopyToDataTableExtensions
    {
        /// <summary>
        /// Copies the collection to a data table.
        /// </summary>
        /// <typeparam name="T">The type of object to be represented in the datatable.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>A new <see cref="System.Data.DataTable"/> instance.</returns>
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        /// <summary>
        /// Copies the collection to a data table.
        /// </summary>
        /// <typeparam name="T">The type of object to be represented in the datatable.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="table">The table.</param>
        /// <param name="options">The options.</param>
        /// <returns>The value of <paramref name="table"/> modified to incorporate the structure of <typeparamref name="T"/> appended with data from <paramref name="source"/>.</returns>
        public static DataTable CopyToDataTable<T>(
            this IEnumerable<T> source,
            DataTable table, 
            LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }
    }
}
