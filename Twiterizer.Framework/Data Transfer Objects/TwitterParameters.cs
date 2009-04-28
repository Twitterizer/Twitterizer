/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2008, Patrick "Ricky" Smith <ricky@digitally-born.com>
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
using System;
using System.Collections.Generic;
using System.Text;

namespace Twitterizer.Framework
{
    public enum TwitterParameterNames
    {
        ID,
        Since,
        SinceID,
        Count,
        Page
    }

    public class TwitterParameters : Dictionary<TwitterParameterNames, object>
    {
        public string BuildActionUri(string Uri)
        {
            if (Count == 0)
                return Uri;

            string parameterString = string.Empty;

            foreach (TwitterParameterNames key in Keys)
            {
                switch (key)
                {
                    case TwitterParameterNames.Since:
                        parameterString = string.Format("{0}&since={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.SinceID:
                        parameterString = string.Format("{0}&since_id={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.Count:
                        parameterString = string.Format("{0}&count={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.Page:
                        parameterString = string.Format("{0}&page={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.ID:
                        parameterString = string.Format("{0}&id={1}", parameterString, this[key]);
                        break;
                }
            }

            if (string.IsNullOrEmpty(parameterString))
                return Uri;

            // First char of parameterString is a leading & that should be removed
            return string.Format("{0}?{1}", Uri, parameterString.Remove(0, 1));
        }

        public new void Add(TwitterParameterNames Key, object Value)
        {
            switch (Key)
            {
                case TwitterParameterNames.Since:
                    if (!(Value is DateTime))
                        throw new ApplicationException("Value given for since was not a Date.");

                    DateTime DateValue = (DateTime)Value;

                    // RFC1123 date string
                    base.Add(Key, DateValue.ToString("r"));

                    break;
                default:
                    base.Add(Key, Value.ToString());
                    break;
            }
        }
    }
}
