using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            TwitterRelationship friendship = TwitterFriendship.Show(tokens, "rickydev");

            // If it exists, delete it.
            if (friendship != null && friendship.Target.IsFollowing.Value)
            {
                DeleteFriendship(tokens, friendship);
            }

            // Create the friendship
            TwitterUser followedUser = TwitterFriendship.Create(tokens, "rickydev");
            Assert.IsNotNull(followedUser);

            // Get the friendship details (maybe again)
            friendship = TwitterFriendship.Show(tokens, "rickydev");
            Assert.IsNotNull(friendship);
            Assert.IsNotNull(friendship.Target);

            // Delete the friendship (maybe again)
            DeleteFriendship(tokens, friendship);
        }

        private static void DeleteFriendship(OAuthTokens tokens, TwitterRelationship friendship)
        {
            TwitterUser unfollowedUser = friendship.Delete(tokens);
            Assert.IsNotNull(unfollowedUser);
        }
    }
}
