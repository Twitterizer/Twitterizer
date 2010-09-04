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
        [Category("Read-Only")]
        [Category("REST")]
        public void GetList()
        {
            TwitterList list = TwitterList.GetList(Configuration.GetTokens(), userName, listName, null).ResponseObject;

            Assert.IsNotNull(list);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public void GetStatuses()
        {
            ListStatusesOptions options = new ListStatusesOptions();
            TwitterStatusCollection statuses = TwitterList.GetStatuses(Configuration.GetTokens(), userName, listName, options).ResponseObject;

            Assert.IsNotNull(statuses);
            Assert.IsNotEmpty(statuses);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public void GetMembers()
        {
            TwitterUserCollection usersInTheList = TwitterList.GetMembers(Configuration.GetTokens(), userName, listName).ResponseObject;

            Assert.IsNotNull(usersInTheList);

            // Attempt to page through the results.
            if (usersInTheList.Count == 20)
            {
                usersInTheList = usersInTheList.NextPage().ResponseObject;
            }
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public static void GetSubscriptions()
        {
            TwitterListCollection lists = TwitterList.GetSubscriptions(Configuration.GetTokens(), userName).ResponseObject;

            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("Read-Only")]
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

            string listName = "test-list-ignore";
            TwitterUser myUser = TwitterAccount.VerifyCredentials(tokens).ResponseObject;
            var userIdToAdd = TwitterUser.Show(tokens, userName).ResponseObject.Id;

            var listResponse = TwitterList.GetList(tokens, myUser.ScreenName, listName);
            if (listResponse.Result == RequestResult.FileNotFound)
            {
                // Create the new list
                listResponse = TwitterList.New(tokens, myUser.ScreenName, listName, false, "Testing Twitterizer");
                Assert.That(listResponse.Result == RequestResult.Success);
            }

            // Add a user
            var addMemberResponse = TwitterList.AddMember(tokens, myUser.ScreenName, listName, userIdToAdd);
            Assert.That(addMemberResponse.Result == RequestResult.Success);

            // Remove the user
            var removeMemberResponse = TwitterList.RemoveMember(tokens, myUser.ScreenName, listName, userIdToAdd);
            Assert.That(removeMemberResponse.Result == RequestResult.Success);

            // Delete the list
            listResponse = TwitterList.Delete(tokens, myUser.ScreenName, listName, null);
            Assert.That(listResponse.Result == RequestResult.Success);
        }
    }
}
