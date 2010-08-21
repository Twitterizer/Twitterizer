using System;
using NUnit.Framework;
using Twitterizer;
using System.Diagnostics;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public static class TwitterStatusTests
    {
        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatus status = TwitterStatus.Show(tokens, 14772516348);
            Assert.IsNotNull(status);
            Assert.IsNotNullOrEmpty(status.Text);
            Assert.That(!status.IsEmpty);
            Assert.That(status != TwitterStatus.Empty);
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void ShowMissing()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatus missingStatus = TwitterStatus.Show(tokens, 1);
            Assert.IsNotNull(missingStatus);
            Assert.That(missingStatus.IsEmpty);
            Assert.That(TwitterStatus.Empty == missingStatus);
        }

        [Category("Read-Write")]
        [Category("REST")]
        [Test]
        public static void UpdateAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            StatusUpdateOptions options = new StatusUpdateOptions();

            TwitterStatus newStatus = TwitterStatus.Update(tokens, "Performing Twitterizer testing ... yes?test=1", options);
            Assert.That(!newStatus.IsEmpty);
            Assert.That(newStatus.Id > 0);

            TwitterStatus deletedStatus = newStatus.Delete(tokens);
            Assert.That(!deletedStatus.IsEmpty);
            Assert.That(newStatus.Id == deletedStatus.Id);
        }

        [Category("Core")]
        [Test]
        [ExpectedException(ExpectedException=typeof(ArgumentException))]
        public static void TestTokenValidation()
        {
            OAuthTokens fakeTokens = new OAuthTokens();
            TwitterStatus.Update(fakeTokens, "This shouldn't work");
        }

        [Category("Core")]
        [Test]
        [ExpectedException(ExpectedException = typeof(ArgumentException))]
        public static void TestTokenValidation2()
        {
            OAuthTokens fakeTokens = new OAuthTokens()
                {
                    ConsumerKey = "fake",
                    ConsumerSecret = "fake"
                };

            TwitterStatus.Update(fakeTokens, "This shouldn't work");
        }
    }
}
