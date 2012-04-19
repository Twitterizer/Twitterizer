using NUnit.Framework;
using Twitterizer;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class TwitterAccountTests
    {
        [Test]
        public static void VerifyCredentials()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterAccount.VerifyCredentials(tokens, new VerifyCredentialsOptions
                                                                        {
                                                                            IncludeEntities = true
                                                                        });

            Assert.IsNotNull(response);
            Assert.That(response.Result == RequestResult.Success);
            Assert.IsNotNull(response.ResponseObject);
        }
    }
}
