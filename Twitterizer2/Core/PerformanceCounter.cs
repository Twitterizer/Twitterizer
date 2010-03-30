//-----------------------------------------------------------------------
// <copyright file="PerformanceCounter.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The performance counter class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Diagnostics;
    using System.Security.Permissions;

    /// <summary>
    /// The available performance counters for Twitterizer
    /// </summary>
    public enum TwitterizerCounter
    {
        /// <summary>
        /// Counts the total number of requests
        /// </summary>
        TotalRequests = 0,

        /// <summary>
        /// Counts the successful requests
        /// </summary>
        TotalSuccessfulRequests = 1,

        /// <summary>
        /// Counts the failed requests
        /// </summary>
        TotalFailedRequests = 2,

        /// <summary>
        /// Counts the OAuth Requests
        /// </summary>
        OAuthRequests = 3,

        /// <summary>
        /// Counts the anonymous requests
        /// </summary>
        AnonymousRequests = 4,

        /// <summary>
        /// The rate of successful requests
        /// </summary>
        SuccessfulRequestsPerSecond = 5,

        /// <summary>
        /// The rate of failed requests
        /// </summary>
        FailedRequestsPerSecond = 6
    }

    internal static class PerformanceCounter
    {
        /// <summary>
        /// Reports a hit to a counter.
        /// </summary>
        /// <param name="counter">The counter.</param>
        internal static void ReportToCounter(TwitterizerCounter twitterizerCounter)
        {
            PerformanceCounterPermission permission = new PerformanceCounterPermission(PerformanceCounterPermissionAccess.Administer, ".", "Twitterizer");
            permission.Demand();

            try
            {
                CreateCounterCategory();
            }
            catch (System.Security.SecurityException)
            {
                return;
            }

            System.Diagnostics.PerformanceCounter newCounter = new System.Diagnostics.PerformanceCounter();
            newCounter.CategoryName = "Twitterizer";
            newCounter.CounterName = GetCounterName(twitterizerCounter);
            newCounter.MachineName = ".";
            newCounter.ReadOnly = false;

            newCounter.Increment();
            newCounter.Close();
        }

        /// <summary>
        /// Creates the counter category.
        /// </summary>
        private static void CreateCounterCategory()
        {
            if (PerformanceCounterCategory.Exists("Twitterizer"))
            {
                return;
            }

            CounterCreationDataCollection counters = new CounterCreationDataCollection();

            counters.Add(new CounterCreationData()
            {
                CounterName = GetCounterName(TwitterizerCounter.TotalFailedRequests),
                CounterType = PerformanceCounterType.NumberOfItems32
            });

            counters.Add(new CounterCreationData()
            {
                CounterName = GetCounterName(TwitterizerCounter.TotalRequests),
                CounterType = PerformanceCounterType.NumberOfItems32
            });

            counters.Add(new CounterCreationData()
            {
                CounterName = GetCounterName(TwitterizerCounter.TotalSuccessfulRequests),
                CounterType = PerformanceCounterType.NumberOfItems32
            });

            counters.Add(new CounterCreationData()
            {
                CounterName = GetCounterName(TwitterizerCounter.OAuthRequests),
                CounterType = PerformanceCounterType.NumberOfItems32
            });

            counters.Add(new CounterCreationData()
            {
                CounterName = GetCounterName(TwitterizerCounter.AnonymousRequests),
                CounterType = PerformanceCounterType.NumberOfItems32
            });

            counters.Add(new CounterCreationData()
            {
                CounterName = GetCounterName(TwitterizerCounter.SuccessfulRequestsPerSecond),
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            });

            counters.Add(new CounterCreationData()
            {
                CounterName = GetCounterName(TwitterizerCounter.FailedRequestsPerSecond),
                CounterType = PerformanceCounterType.RateOfCountsPerSecond32
            });

            PerformanceCounterCategory.Create(
                "Twitterizer", 
                "Twitterizer class library performance counters.", 
                PerformanceCounterCategoryType.SingleInstance, 
                counters);
        }

        /// <summary>
        /// Gets the name of the counter.
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns></returns>
        private static string GetCounterName(TwitterizerCounter counter)
        {
            switch (counter)
            {
                case TwitterizerCounter.TotalRequests:
                    return "Total # of Requests";
                case TwitterizerCounter.TotalSuccessfulRequests:
                    return "Total # of Successful Requests";
                    break;
                case TwitterizerCounter.TotalFailedRequests:
                    return "Total # of Failed Requests";
                case TwitterizerCounter.OAuthRequests:
                    return "# of OAuth Requests";
                case TwitterizerCounter.AnonymousRequests:
                    return "# of Anonymous Requests";
                case TwitterizerCounter.SuccessfulRequestsPerSecond:
                    return "Successful Requests Per Second";
                case TwitterizerCounter.FailedRequestsPerSecond:
                    return "Failed Requests Per Second";
                default:
                    return string.Empty;
            }
        }
    }
}
