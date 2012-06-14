using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer;
using Twitterizer2.MSTestCases;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class TwitterListTests
    {
        private const string userName = "twitterapi";
        private const string listName = "meetup-20100301";

        [TestMethod]
        public void Show()
        {
            var list = TwitterList.Show(Configuration.GetTokens(), listName, null);

            Assert.IsNotNull(list.ResponseObject, list.ErrorMessage);
        }

        [TestMethod]
        public void GetStatuses()
        {
            ListStatusesOptions options = new ListStatusesOptions();
            TwitterResponse<TwitterStatusCollection> statuses = TwitterList.GetStatuses(Configuration.GetTokens(), userName, listName, options);

            Assert.IsNotNull(statuses, "statuses is null");
            Assert.IsTrue(statuses.Result == RequestResult.Success, statuses.ErrorMessage ?? statuses.Result.ToString());
        }

        [TestMethod]
        public void GetMembers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterList> list = TwitterList.Show(tokens, "ghc10-attendees");
            TwitterResponse<TwitterUserCollection> usersInTheList = TwitterList.GetMembers(tokens, "ghc", "ghc10-attendees");

            Assert.IsNotNull(usersInTheList, "usersInTheList is null");
            Assert.IsTrue(usersInTheList.Result == RequestResult.Success, usersInTheList.ErrorMessage ?? usersInTheList.Result.ToString());
            Assert.IsNotNull(usersInTheList.ResponseObject, usersInTheList.ErrorMessage ?? usersInTheList.Result.ToString());

            int countedMembers = usersInTheList.ResponseObject.Count;

            // Attempt to page through the results.
            while (usersInTheList != null && usersInTheList.ResponseObject.Count > 0)
            {
                usersInTheList = TwitterList.GetMembers(tokens, "ghc", "ghc10-attendees", new GetListMembersOptions { Cursor = usersInTheList.ResponseObject.NextCursor });
                Assert.IsTrue(usersInTheList.Result == RequestResult.Success, usersInTheList.ErrorMessage ?? usersInTheList.Result.ToString());
                Assert.IsNotNull(usersInTheList.ResponseObject, usersInTheList.ErrorMessage ?? usersInTheList.Result.ToString());

                if (usersInTheList != null)
                    countedMembers += usersInTheList.ResponseObject.Count;
            }

            Assert.IsTrue(countedMembers == list.ResponseObject.NumberOfMembers);
        }

        [TestMethod]
        public void GetSubscriptions()
        {
            var response = TwitterList.GetSubscriptions(Configuration.GetTokens(), userName);
            TwitterListCollection lists = response.ResponseObject;

            Assert.IsNotNull(lists, response.ErrorMessage);
        }

        [TestMethod]
        public void GetMemberships()
        {
            var response = TwitterList.GetMemberships(Configuration.GetTokens(), userName);
            TwitterListCollection lists = response.ResponseObject;

            Assert.IsNotNull(lists, response.ErrorMessage);
        }

        [TestMethod]
        public void CreateAddAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            string testListIgnore = "test-list-ignore";
            var myUser = TwitterAccount.VerifyCredentials(tokens);

            Assert.IsNotNull(myUser.ResponseObject, myUser.ErrorMessage);

            var userIdToAdd = TwitterUser.Show(tokens, userName).ResponseObject.Id;

            var listResponse = TwitterList.Show(tokens, testListIgnore);
            if (listResponse.Result == RequestResult.FileNotFound)
            {
                // Create the new list
                listResponse = TwitterList.New(tokens, myUser.ResponseObject.ScreenName, testListIgnore, false, "Testing Twitterizer");
                Assert.IsTrue(listResponse.Result == RequestResult.Success);
            }

            // Add a user
            var addMemberResponse = TwitterList.AddMember(tokens, myUser.ResponseObject.ScreenName, testListIgnore, userIdToAdd);
            Assert.IsTrue(addMemberResponse.Result == RequestResult.Success);

            // Remove the user
            var removeMemberResponse = TwitterList.RemoveMember(tokens, myUser.ResponseObject.ScreenName, testListIgnore, userIdToAdd);
            Assert.IsTrue(removeMemberResponse.Result == RequestResult.Success);

            // Delete the list
            listResponse = TwitterList.Delete(tokens, myUser.ResponseObject.ScreenName, testListIgnore, null);
            Assert.IsTrue(listResponse.Result == RequestResult.Success);
        }

        [TestMethod]
        public void CheckMembership()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterUser> secondUserResponse = TwitterUser.Show(tokens, "episod");
            Assert.IsNotNull(secondUserResponse, secondUserResponse.ErrorMessage ?? secondUserResponse.Result.ToString());
            Assert.IsTrue(secondUserResponse.Result == RequestResult.Success, secondUserResponse.ErrorMessage ?? secondUserResponse.Result.ToString());

            
            TwitterResponse<TwitterUser> membershipResponse = TwitterList.CheckMembership(
                tokens,
                "twitterapi",
                "team",
                secondUserResponse.ResponseObject.Id);

            Assert.IsNotNull(membershipResponse, membershipResponse.ErrorMessage ?? membershipResponse.Result.ToString());
            Assert.IsTrue(membershipResponse.Result == RequestResult.Success, membershipResponse.ErrorMessage ?? membershipResponse.Result.ToString());
        }
    }
}
