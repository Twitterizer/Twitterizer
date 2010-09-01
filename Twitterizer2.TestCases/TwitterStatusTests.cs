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

            TwitterResponse<TwitterStatus> response = TwitterStatus.Show(tokens, 14772516348);
            TwitterStatus status = response.ResponseObject;
            Assert.IsNotNull(status);
            Assert.IsNotNullOrEmpty(status.Text);
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void ShowMissing()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatus missingStatus = TwitterStatus.Show(tokens, 1).ResponseObject;
            Assert.IsNotNull(missingStatus);
        }

        [Category("Read-Write")]
        [Category("REST")]
        [Test]
        public static void UpdateAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            StatusUpdateOptions options = new StatusUpdateOptions();

            TwitterStatus newStatus = TwitterStatus.Update(tokens, "Performing Twitterizer testing ...", options).ResponseObject;
            Assert.That(newStatus.Id > 0);

            TwitterStatus deletedStatus = newStatus.Delete(tokens).ResponseObject;
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

        [Test]
        public static void UpdateWithURLParameters()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatus> response = TwitterStatus.Update(tokens, WebRequestBuilder.UrlEncode("This is a test. http://example.com/test?param=value"));
            Assert.IsNotNull(response.ResponseObject);

        }
    }
}
