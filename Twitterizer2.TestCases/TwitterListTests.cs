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
        private TwitterList List;
        private OAuthTokens Tokens;

        [TestFixtureSetUp]
        public void Setup()
        {
            this.Tokens = Configuration.GetTokens();

            TwitterListCollection lists = TwitterList.GetLists(this.Tokens, "twit_er_izer");

            if (lists != null)
                this.List = lists.FirstOrDefault();

            Assert.IsNotNull(this.List);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public void GetStatuses()
        {
            if (this.List != null)
            {
                ListStatusesOptions options = new ListStatusesOptions();
                TwitterStatusCollection statuses = TwitterList.GetStatuses(this.Tokens, "twit_er_izer", this.List.Id, options);

                Assert.IsNotNull(statuses);
                Assert.IsNotEmpty(statuses);
            }
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public void GetMembers()
        {
            if (this.List != null)
            {
                TwitterUserCollection usersInTheList = TwitterList.GetMembers(this.Tokens, "twit_er_izer", this.List.Id);

                Assert.IsNotNull(usersInTheList);
            }
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public static void GetSubscriptions()
        {
            TwitterListCollection lists = TwitterList.GetSubscriptions(Configuration.GetTokens(), "twit_er_izer");

            Assert.IsNotNull(lists);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        public static void GetMemberships()
        {
            TwitterListCollection lists = TwitterList.GetMemberships(Configuration.GetTokens(), "twit_er_izer");

            Assert.IsNotNull(lists);
        }
    }
}
