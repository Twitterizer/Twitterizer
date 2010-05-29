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
        [Test]
        public static void GetStatuses()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterList firstList = TwitterList.GetLists(tokens, "twit_er_izer").FirstOrDefault();

            Assert.IsNotNull(firstList);
            if (firstList != null)
            {
                ListStatusesOptions options = new ListStatusesOptions();
                TwitterStatusCollection statuses = TwitterList.GetStatuses(tokens, "twit_er_izer", firstList.Id, options);

                Assert.IsNotNull(statuses);
                Assert.IsNotEmpty(statuses);
            }
        }
    }
}
