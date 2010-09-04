//-----------------------------------------------------------------------
// <copyright file="OptionalProperties.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The base class for optional property classes</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using System.Configuration;
    using System.Net;

    /// <include file='OptionalProperties.xml' path='OptionalProperties/OptionalProperties/*'/>
    [Serializable]
    public class OptionalProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalProperties"/> class.
        /// </summary>
        public OptionalProperties()
        {
            // Set the default values for the properties
            this.UseSSL = false;
            this.APIBaseAddress = "http://api.twitter.com/1/";

#if !LITE
            this.CacheOutput = false;
            this.CacheTimespan = new TimeSpan(0, 5, 0);
            this.ReadConfigurationSettings();
#endif
        }

        /// <include file='OptionalProperties.xml' path='OptionalProperties/Property[@name="UseSSL"]/*'/>
        /// <summary>
        ///  Allows configuration of the base address for API method requests for support for 3rd party 'twitter-like' APIs.
        /// </summary>
        public bool UseSSL { get; set; }

        /// <include file='OptionalProperties.xml' path='OptionalProperties/Property[@name="APIBaseAddress"]/*'/>
        public string APIBaseAddress { get; set; }

        /// <include file='OptionalProperties.xml' path='OptionalProperties/Property[@name="Proxy"]/*'/>
        public WebProxy Proxy { get; set; }

#if !LITE
        /// <include file='OptionalProperties.xml' path='OptionalProperties/Property[@name="CacheOutput"]/*'/>
        public bool CacheOutput { get; set; }

        /// <include file='OptionalProperties.xml' path='OptionalProperties/Property[@name="CacheTimespan"]/*'/>
        public TimeSpan CacheTimespan { get; set; }

        /// <summary>
        /// Reads the configuration settings.
        /// </summary>
        private void ReadConfigurationSettings()
        {

            System.Collections.Specialized.NameValueCollection appSettings = ConfigurationManager.AppSettings;

            // Get the enable caching configuration setting
            if (!string.IsNullOrEmpty(appSettings["Twitterizer2.EnableCaching"]) &&
                appSettings["Twitterizer2.EnableCaching"].ToLower().Trim() == "true")
            {
                this.CacheOutput = true;
            }

            // Get the cache timeout setting
            string cacheTimeoutSetting = appSettings["Twitterizer2.CacheTimeout"];
            long cacheTimeoutSeconds;
            if (!string.IsNullOrEmpty(appSettings["Twitterizer2.CacheTimeout"]) &&
                long.TryParse(cacheTimeoutSetting, out cacheTimeoutSeconds))
            {
                // Convert the seconds value to ticks and instantiate a new timespan
                this.CacheTimespan = new TimeSpan(cacheTimeoutSeconds * 10000000);
            }

            // Get the SSL setting
            if (!string.IsNullOrEmpty(appSettings["Twitterizer2.EnableSSL"]) &&
                appSettings["Twitterizer2.EnableSSL"].ToLower().Trim() == "true")
            {
                this.UseSSL = true;
            }
        }
#endif
        }
}
