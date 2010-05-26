namespace Twitterizer2.TestCases
{
    using NUnit.Framework;
    using Twitterizer;
    
    [TestFixture]
    public class TwitterUserTests
    {
        [Test]
        public static void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUser user = TwitterUser.Show(tokens, "twit_er_izer");
            Assert.IsNotNull(user);
            Assert.That(!string.IsNullOrEmpty(user.ScreenName));

            Assert.IsNotNull(user.Status);
            Assert.That(!string.IsNullOrEmpty(user.Status.Text));
        }

        [Test]
        public static void Search()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUserCollection results = TwitterUser.Search(tokens, "twit_er_izer");
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }
    }
}
