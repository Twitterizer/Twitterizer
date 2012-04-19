using NUnit.Framework;
using Twitterizer;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class UserSuggestionTests
    {
        [Test]
        public static void GetCategoriesAndUsers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            // Get the list of categories (no user data)
            var categoryResponse = TwitterUserCategory.SuggestedUserCategories(tokens);

            Assert.IsNotNull(categoryResponse);
            Assert.That(categoryResponse.Result == RequestResult.Success);
            Assert.IsNotNull(categoryResponse.ResponseObject);
            Assert.IsNotEmpty(categoryResponse.ResponseObject);

            // Get a single category (with users)
            var usersResponse = TwitterUserCategory.SuggestedUsers(tokens, categoryResponse.ResponseObject[0].Slug);

            Assert.IsNotNull(usersResponse);
            Assert.That(usersResponse.Result == RequestResult.Success);
            Assert.IsNotNull(usersResponse.ResponseObject);
            Assert.IsNotNull(usersResponse.ResponseObject.Users);
            Assert.IsNotEmpty(usersResponse.ResponseObject.Users);
        }
    }
}
