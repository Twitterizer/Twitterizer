using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer;
using Twitterizer2.MSTestCases;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class TwitterAccountTests
    {
        [TestMethod]
        public void VerifyCredentials()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterAccount.VerifyCredentials(tokens, new VerifyCredentialsOptions
                                                                        {
                                                                            IncludeEntities = true
                                                                        });

            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage);
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
        }
    }
}
