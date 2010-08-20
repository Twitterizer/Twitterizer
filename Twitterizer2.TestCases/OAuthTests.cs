using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Twitterizer;
using System.Net;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class OAuthTests
    {
        [Test]
        public static void RequestToken()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            OAuthTokenResponse response = OAuthUtility.GetRequestToken(tokens.ConsumerKey, tokens.ConsumerSecret, string.Empty);

            Assert.IsNotNull(response);
            Assert.IsNotNullOrEmpty(response.Token);
        }
    }
}
