using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Twitterizer;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public static class UserIdListTests
    {
        [Test]
        public static void GetFriendIds()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterFriendship.FriendsIds(tokens, new UsersIdsOptions());
            Assert.IsNotNull(response);
            Assert.That(response.Result == RequestResult.Success);
            Assert.IsNotNull(response.ResponseObject);
            Assert.IsNotEmpty(response.ResponseObject);
        }
    }
}
