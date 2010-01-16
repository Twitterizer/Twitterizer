using System;
using System.Collections.Generic;
using System.Text;

namespace Twitterizer.Framework
{
    public abstract class TwitterMethodBase
    {
        internal Uri BuildConditionalUrl(TwitterParameters Parameters, string Path)
        {
            if (Parameters == null || Parameters.Count == 0)
            {
                return new Uri(string.Concat(Twitter.Domain, Path));
            }

            return new Uri(Parameters.BuildActionUri("direct_messages.xml"));
        }
    }
}
