using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer;
using Twitterizer2.MSTestCases;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class UserSuggestionTests
    {
        [TestMethod]
        public void GetCategoriesAndUsers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            // Get the list of categories (no user data)
            var categoryResponse = TwitterUserCategory.SuggestedUserCategories(tokens);

            Assert.IsNotNull(categoryResponse, "categoryResponse is null");
            Assert.IsTrue(categoryResponse.Result == RequestResult.Success, categoryResponse.ErrorMessage);
            Assert.IsNotNull(categoryResponse.ResponseObject, categoryResponse.ErrorMessage);
            Assert.AreNotEqual(0, categoryResponse.ResponseObject.Count, categoryResponse.ErrorMessage);

            // Get a single category (with users)
            var usersResponse = TwitterUserCategory.SuggestedUsers(tokens, categoryResponse.ResponseObject[0].Slug);

            Assert.IsNotNull(usersResponse, "usersResponse is null");
            Assert.IsTrue(usersResponse.Result == RequestResult.Success, usersResponse.ErrorMessage);
            Assert.IsNotNull(usersResponse.ResponseObject, usersResponse.ErrorMessage);
            Assert.IsNotNull(usersResponse.ResponseObject.Users, usersResponse.ErrorMessage);
            Assert.AreNotEqual(0, usersResponse.ResponseObject.Users.Count, usersResponse.ErrorMessage);
        }
    }
}
