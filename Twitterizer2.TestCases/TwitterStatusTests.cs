using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Twitterizer;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public static class TwitterStatusTests
    {
        [Test]
        public static void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatus missingStatus = TwitterStatus.Show(tokens, 1);
            Assert.IsNull(missingStatus);

            TwitterStatus status = TwitterStatus.Show(tokens, 14772516348);
            Assert.IsNotNull(status);
            Assert.IsNotNullOrEmpty(status.Text);
        }
    }
}
