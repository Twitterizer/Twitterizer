using Twitterizer;
using NUnit.Framework;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class TwitterFriendshipTests
    {
        [Ignore("Tests that POST or DELETE should be tested one at a time.")]
        [Category("Read-Write")]
        [Category("Friendship")]
        [Test]
        public static void ShowCreateAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            // See if the friendship exists (it should not)
            TwitterRelationship friendship = TwitterFriendship.Show(tokens, "rickydev").ResponseObject;

            // If it exists, delete it.
            if (friendship.Target.Following)
            {
                DeleteFriendship(tokens, friendship);
            }

            // Create the friendship
            TwitterUser followedUser = TwitterFriendship.Create(tokens, "rickydev").ResponseObject;
            Assert.IsNotNull(followedUser);

            // Get the friendship details (maybe again)
            friendship = TwitterFriendship.Show(tokens, "rickydev").ResponseObject;
            Assert.IsNotNull(friendship);
            Assert.IsNotNull(friendship.Target);

            // Delete the friendship (maybe again)
            DeleteFriendship(tokens, friendship);
        }

        private static void DeleteFriendship(OAuthTokens tokens, TwitterRelationship friendship)
        {
            TwitterUser unfollowedUser = friendship.Delete(tokens).ResponseObject;
            Assert.IsNotNull(unfollowedUser);
        }

        /// <summary>
        /// This is a test for code submitted in the forums by reradus.
        /// </summary>
        [Test]
        public static void ReradusTest()
        {
            //OAuthTokens tokens = Configuration.GetTokens();

            FollowersOptions options = new FollowersOptions();
            //options.ScreenName = _Screenname;
            options.UserId = 189996115;
            var followers = TwitterFriendship.Followers(options);
        }
    }
}
