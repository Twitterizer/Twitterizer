using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Twitterizer
{
    public static class WebExceptionExtensions
    {
        public static TwitterizerException ToTwitterizerException(this WebException wex)
        {
            return new TwitterizerException(wex);
        }
    }
}
