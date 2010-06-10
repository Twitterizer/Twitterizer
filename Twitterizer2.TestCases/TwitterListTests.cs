using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Twitterizer;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class TwitterListTests
    {
        private const string userName = "twitterapi";
        private const string listName = "team";

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public void GetList()
        {
            TwitterList list = TwitterList.GetList(Configuration.GetTokens(), userName, listName, null);

            Assert.IsNotNull(list);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public void GetStatuses()
        {
            ListStatusesOptions options = new ListStatusesOptions();
            TwitterStatusCollection statuses = TwitterList.GetStatuses(Configuration.GetTokens(), userName, listName, options);

            Assert.IsNotNull(statuses);
            Assert.IsNotEmpty(statuses);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public void GetMembers()
        {
            TwitterUserCollection usersInTheList = TwitterList.GetMembers(Configuration.GetTokens(), userName, listName);

            Assert.IsNotNull(usersInTheList);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public static void GetSubscriptions()
        {
            TwitterListCollection lists = TwitterList.GetSubscriptions(Configuration.GetTokens(), userName);

            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public static void GetMemberships()
        {
            TwitterListCollection lists = TwitterList.GetMemberships(Configuration.GetTokens(), userName);

            Assert.IsNotNull(lists);
        }
    }
}
