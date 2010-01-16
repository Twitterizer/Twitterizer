using System;
using System.Collections.Generic;
using System.Text;

namespace Twitterizer.Framework
{
    public abstract class TwitterObject
    {
        // Rate Limit Properties
        public int? RateLimit { get; set; }
        public int? RateLimitRemaining { get; set; }
        public DateTime? RateLimitReset { get; set; }
    }
}
