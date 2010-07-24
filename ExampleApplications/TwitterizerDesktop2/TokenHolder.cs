using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterizerDesktop2
{
    public sealed class TokenHolder
    {
        static readonly TokenHolder instance = new TokenHolder();

        public static TokenHolder Instance
        {
            get
            {
                return instance;
            }
        }

        static TokenHolder()
        {
        }

        TokenHolder()
        {
        }

        public Twitterizer.OAuthTokenResponse AccessTokenResponse { get; set; }
    }
}