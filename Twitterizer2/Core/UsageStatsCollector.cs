//-----------------------------------------------------------------------
// <copyright file="UsageStatsCollector.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The usage statistics collector class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Security;
    using System.Security.Permissions;
    using System.Web;

    /// <summary>
    /// The Usage Statistics Collector class
    /// </summary>
    internal static class UsageStatsCollector
    {
        /// <summary>
        /// The Report Call Delegate for async reporting
        /// </summary>
        /// <param name="apiMethodUri">The API method being called.</param>
        private delegate void ReportCaller(string apiMethodUri);

        /// <summary>
        /// Reports the call async.
        /// </summary>
        /// <param name="apiMethodUri">The API method URI.</param>
        public static void ReportCallAsync(string apiMethodUri)
        {
            Trace.Write("Begin Usage Stats Collection Async", "Twitterizer2");

            // Make sure statistics collection hasn't been disabled
            string configValue = ConfigurationManager.AppSettings["Twitterizer2.EnableStatisticsCollection"];
            if (!string.IsNullOrEmpty(configValue) && configValue.ToLower(CultureInfo.CurrentCulture) == "false")
            {
                return;
            }

            try
            {
                // You can thank Dirm from irc://chat.freenode.net/##csharp for helping me with this security permission check
                // Without it, the report call method isn't executed in Medium trust
                if (SecurityManager.IsGranted(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode)))
                {
                    ReportCaller caller = new ReportCaller(ReportCall);
                    caller.BeginInvoke(apiMethodUri, null, null);
                }
                else
                {
                    ReportCall(apiMethodUri);
                }

#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    string.Format(
                        CultureInfo.CurrentCulture, 
                        "Reported method: {0}", 
                        apiMethodUri));
#endif
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
#if DEBUG
                System.Diagnostics.Debug.WriteLine(
                    string.Format(
                        CultureInfo.CurrentCulture, 
                        "Failed to report method: {0} ({1})", 
                        apiMethodUri, 
                        ex.Message));
#endif
            }

            Trace.Write("End Usage Stats Collection Async", "Twitterizer2");
        }

        /// <summary>
        /// Reports the call.
        /// </summary>
        /// <param name="apiMethodUri">The API method URI.</param>
        public static void ReportCall(string apiMethodUri)
        {
            string configValue = ConfigurationManager.AppSettings["Twitterizer2.EnableStatisticsCollection"];
            if (!string.IsNullOrEmpty(configValue) && configValue.ToLower(CultureInfo.CurrentCulture) == "false")
            {
                return;
            }

            Trace.Write("Begin Usage Stats Collection", "Twitterizer2");

            string platform;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                    platform = "MacOSX";
                    break;
                case PlatformID.Unix:
                    platform = "Unix";
                    break;
                case PlatformID.Win32NT:
                    platform = "Win32NT";
                    break;
                case PlatformID.Win32S:
                    platform = "Win32S";
                    break;
                case PlatformID.Win32Windows:
                    platform = "Win32Windows";
                    break;
                case PlatformID.WinCE:
                    platform = "WinCE";
                    break;
                case PlatformID.Xbox:
                    platform = "Xbox";
                    break;
                default:
                    platform = "Unknown";
                    break;
            }

            platform = string.Format(CultureInfo.CurrentCulture, "{0} ({1})", platform, Environment.OSVersion.Version);

            string version = typeof(Twitterizer.Core.TwitterObject).Assembly.GetName().Version.ToString();
            string processName = string.Empty;
            string framework = Environment.Version.ToString();

            try
            {
                if (SecurityManager.IsGranted(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode)))
                {
                    processName = GetProcessName();
                }
            }
            catch (SecurityException)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Lacking ReflectionPermission.");
#endif
            }

            string collectionAddress = string.Format(
                CultureInfo.CurrentCulture,
                "http://www.twitterizer.net/collect?framework={0}&method={1}&platform={2}&version={3}&process={4}",
                HttpUtility.UrlEncode(framework),
                HttpUtility.UrlEncode(apiMethodUri),
                HttpUtility.UrlEncode(platform),
                HttpUtility.UrlEncode(version),
                HttpUtility.UrlEncode(processName));

#if DEBUG
            System.Diagnostics.Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Reporting Address: {0}", collectionAddress));
#endif

            try
            {
                WebPermission permission = new WebPermission();
                permission.AddPermission(NetworkAccess.Connect, @"http://www.twitterizer.net/.*");
                permission.Demand();

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(collectionAddress);
                request.GetResponse();
            }
            catch (WebException)
            {
            }
            catch (SecurityException)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Lacking WebPermission.");
#endif
            }

            Trace.Write("End Usage Stats Collection Async", "Twitterizer2");
        }

        /// <summary>
        /// Gets the name of the process.
        /// </summary>
        /// <returns>string; The process name.</returns>
        private static string GetProcessName()
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        }
    }
}
