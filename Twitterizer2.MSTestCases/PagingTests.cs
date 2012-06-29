namespace Twitterizer2.MSTestCases
{
    using Twitterizer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Twitterizer2.MSTestCases;
    
    [TestClass]
    public class PagingTests
    {
        [TestMethod]
        public void Mentions()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> response = TwitterTimeline.Mentions(tokens);

            Assert.IsNotNull(response , "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());

            decimal firstId = response.ResponseObject[0].Id;

            response = TwitterTimeline.Mentions(tokens, new TimelineOptions { Page = ++response.ResponseObject.Page });
            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());
            Assert.AreNotEqual(response.ResponseObject[0].Id, firstId, response.ErrorMessage ?? response.Result.ToString());
        }

        [TestMethod]
        public void UserTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> response = TwitterTimeline.UserTimeline(tokens);

            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());

            decimal firstId = response.ResponseObject[0].Id;

            response = TwitterTimeline.UserTimeline(tokens, new UserTimelineOptions { Page = ++response.ResponseObject.Page });
            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());
            Assert.AreNotEqual(response.ResponseObject[0].Id, firstId, response.ErrorMessage ?? response.Result.ToString());
        }

        [TestMethod]
        public void Friends()
        {
            OAuthTokens tokens = Configuration.GetTokens();

#pragma warning disable 618
            TwitterResponse<TwitterStatusCollection> response = TwitterTimeline.FriendTimeline(tokens, new TimelineOptions { Count = 2 });
#pragma warning restore 618

            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());

            decimal firstId = response.ResponseObject[0].Id;

#pragma warning disable 618
            response = TwitterTimeline.FriendTimeline(tokens, new TimelineOptions { Page = ++response.ResponseObject.Page });
#pragma warning restore 618
            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success);
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());
            Assert.AreNotEqual(response.ResponseObject[0].Id, firstId, response.ErrorMessage ?? response.Result.ToString());
        }

        [TestMethod]
        public void Home()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> response = TwitterTimeline.HomeTimeline(tokens);

            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());

            decimal firstId = response.ResponseObject[0].Id;

            response = TwitterTimeline.HomeTimeline(tokens, new TimelineOptions { Page = ++response.ResponseObject.Page });
            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());
            Assert.AreNotEqual(response.ResponseObject[0].Id, firstId, response.ErrorMessage ?? response.Result.ToString());
        }

        [TestMethod]
        public void Followers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterUserCollection> response = TwitterFriendship.Followers(tokens, new FollowersOptions { ScreenName = "twitterapi" });

            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());

            decimal firstId = response.ResponseObject[0].Id;

            response = TwitterFriendship.Followers(tokens, new FollowersOptions { ScreenName = "twitterapi", Cursor = response.ResponseObject.NextCursor });
            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());
            Assert.AreNotEqual(response.ResponseObject[0].Id, firstId, response.ErrorMessage ?? response.Result.ToString());
        }

        [TestMethod]
        public void FollowersIds()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<UserIdCollection> response = TwitterFriendship.FollowersIds(tokens, new UsersIdsOptions
            {
                ScreenName = "twitterapi"
            });

            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());

            decimal firstId = response.ResponseObject[0];

            response = TwitterFriendship.FollowersIds(tokens, new UsersIdsOptions { ScreenName = "twitterapi", Cursor = response.ResponseObject.NextCursor });
            Assert.IsNotNull(response, "response is null");
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage ?? response.Result.ToString());
            Assert.IsTrue(response.ResponseObject.Count > 0, response.ErrorMessage ?? response.Result.ToString());
            Assert.AreNotEqual(response.ResponseObject[0], firstId, response.ErrorMessage ?? response.Result.ToString());
        }
    }
}
