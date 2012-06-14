using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer;
using Twitterizer2.MSTestCases;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class UserIdListTests
    {
        [TestMethod]
        public void GetFriendIds()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterFriendship.FriendsIds(tokens, new UsersIdsOptions());
            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage);
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
            Assert.AreNotEqual(0, response.ResponseObject.Count, response.ErrorMessage);
        }
    }
}
