using NUnit.Framework;
using Twitterizer;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class TwitterListTests
    {
        private const string userName = "twitterapi";
        private const string listName = "meetup-20100301";

        [Test]
        [Category("ReadOnly")]
        [Category("REST")]
        public void Show()
        {
            TwitterList list = TwitterList.Show(Configuration.GetTokens(), listName, null).ResponseObject;

            Assert.IsNotNull(list);
        }

        [Test]
        [Category("ReadOnly")]
        [Category("REST")]
        public void GetStatuses()
        {
            ListStatusesOptions options = new ListStatusesOptions();
            TwitterResponse<TwitterStatusCollection> statuses = TwitterList.GetStatuses(Configuration.GetTokens(), userName, listName, options);

            Assert.IsNotNull(statuses);
            Assert.That(statuses.Result == RequestResult.Success);
        }

        [Test]
        [Category("ReadOnly")]
        [Category("REST")]
        public void GetMembers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterList> list = TwitterList.Show(tokens, "ghc10-attendees");
            TwitterResponse<TwitterUserCollection> usersInTheList = TwitterList.GetMembers(tokens, "ghc", "ghc10-attendees");

            Assert.IsNotNull(usersInTheList);
            Assert.That(usersInTheList.Result == RequestResult.Success);

            int countedMembers = usersInTheList.ResponseObject.Count;

            // Attempt to page through the results.
            while (usersInTheList != null && usersInTheList.ResponseObject.Count > 0)
            {
                usersInTheList = TwitterList.GetMembers(tokens, "ghc", "ghc10-attendees", new GetListMembersOptions { Cursor = usersInTheList.ResponseObject.NextCursor });
                
                if (usersInTheList != null)
                    countedMembers += usersInTheList.ResponseObject.Count;
            }

            Assert.That(countedMembers == list.ResponseObject.NumberOfMembers);
        }

        [Test]
        [Category("ReadOnly")]
        [Category("REST")]
        public static void GetSubscriptions()
        {
            TwitterListCollection lists = TwitterList.GetSubscriptions(Configuration.GetTokens(), userName).ResponseObject;

            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("ReadOnly")]
        [Category("REST")]
        public static void GetMemberships()
        {
            TwitterListCollection lists = TwitterList.GetMemberships(Configuration.GetTokens(), userName).ResponseObject;

            Assert.IsNotNull(lists);
        }

        [Test]
        public static void CreateAddAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            string testListIgnore = "test-list-ignore";
            TwitterUser myUser = TwitterAccount.VerifyCredentials(tokens).ResponseObject;
            var userIdToAdd = TwitterUser.Show(tokens, userName).ResponseObject.Id;

            var listResponse = TwitterList.Show(tokens, testListIgnore);
            if (listResponse.Result == RequestResult.FileNotFound)
            {
                // Create the new list
                listResponse = TwitterList.New(tokens, myUser.ScreenName, testListIgnore, false, "Testing Twitterizer");
                Assert.That(listResponse.Result == RequestResult.Success);
            }

            // Add a user
            var addMemberResponse = TwitterList.AddMember(tokens, myUser.ScreenName, testListIgnore, userIdToAdd);
            Assert.That(addMemberResponse.Result == RequestResult.Success);

            // Remove the user
            var removeMemberResponse = TwitterList.RemoveMember(tokens, myUser.ScreenName, testListIgnore, userIdToAdd);
            Assert.That(removeMemberResponse.Result == RequestResult.Success);

            // Delete the list
            listResponse = TwitterList.Delete(tokens, myUser.ScreenName, testListIgnore, null);
            Assert.That(listResponse.Result == RequestResult.Success);
        }

        [Test]
        public static void CheckMembership()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterUser> secondUserResponse = TwitterUser.Show(tokens, "episod");
            Assert.IsNotNull(secondUserResponse);
            Assert.That(secondUserResponse.Result == RequestResult.Success);

            
            TwitterResponse<TwitterUser> membershipResponse = TwitterList.CheckMembership(
                tokens,
                "twitterapi",
                "team",
                secondUserResponse.ResponseObject.Id);

            Assert.IsNotNull(membershipResponse);
            Assert.That(membershipResponse.Result == RequestResult.Success);
        }
    }
}
