using Twitterizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer2.MSTestCases;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class TwitterFriendshipTests
    {
        [Ignore()] // Tests that POST or DELETE should be tested one at a time
        [TestMethod]
        public void ShowCreateAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            // See if the friendship exists (it should not)
            var friendship = TwitterFriendship.Show(tokens, "rickydev");

            // If it exists, delete it.
            if (friendship.ResponseObject.Target.Following)
            {
                DeleteFriendship(tokens, friendship.ResponseObject);
            }

            // Create the friendship
            var followedUser = TwitterFriendship.Create(tokens, "rickydev");
            Assert.IsNotNull(followedUser.ResponseObject, followedUser.ErrorMessage);

            // Get the friendship details (maybe again)
            friendship = TwitterFriendship.Show(tokens, "rickydev");
            Assert.IsNotNull(friendship.ResponseObject, friendship.ErrorMessage);
            Assert.IsNotNull(friendship.ResponseObject.Target, friendship.ErrorMessage);

            // Delete the friendship (maybe again)
            DeleteFriendship(tokens, friendship.ResponseObject);
        }

        private static void DeleteFriendship(OAuthTokens tokens, TwitterRelationship friendship)
        {
            var unfollowedUser = friendship.Delete(tokens);
            Assert.IsNotNull(unfollowedUser.ResponseObject, unfollowedUser.ErrorMessage);
        }

        /// <summary>
        /// This is a test for code submitted in the forums by reradus.
        /// </summary>
        [TestMethod]
        public void ReradusTest()
        {
            //OAuthTokens tokens = Configuration.GetTokens();

            FollowersOptions options = new FollowersOptions();
            //options.ScreenName = _Screenname;
            options.UserId = 189996115;
            var followers = TwitterFriendship.Followers(options);

            Assert.IsNotNull(followers.ResponseObject, followers.ErrorMessage);
        }
    }
}
