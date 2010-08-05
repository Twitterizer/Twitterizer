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
    }
}
