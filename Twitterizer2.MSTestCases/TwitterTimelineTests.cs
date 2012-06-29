using System;
using Twitterizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class TwitterTimelineTests
    {
        [TestMethod]
        public void PublicTimeline()
        {
            var response = TwitterTimeline.PublicTimeline();
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
            Assert.AreNotEqual(0, response.ResponseObject.Count, response.ErrorMessage);

            Assert.IsTrue(response.ResponseObject.Count > 0 && response.ResponseObject.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [TestMethod]
        public void UserTimeline()
        {
            IAsyncResult asyncResult = TwitterTimelineAsync.UserTimeline(
                Configuration.GetTokens(),
                new UserTimelineOptions(),
                new TimeSpan(0, 2, 0),
                PerformCommonTimelineTests);

            asyncResult.AsyncWaitHandle.WaitOne();

            UserTimelineOptions User_Options = new UserTimelineOptions();
            User_Options.ScreenName = "twitterapi";
            User_Options.Count = 8;

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.UserTimeline(Configuration.GetTokens(), User_Options);
            PerformCommonTimelineTests(timelineResponse);

            timelineResponse = TwitterTimeline.UserTimeline(User_Options);
            PerformCommonTimelineTests(timelineResponse);
        }

        [TestMethod]
        public void FriendTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

#pragma warning disable 618
            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.FriendTimeline(tokens);
#pragma warning restore 618
            PerformCommonTimelineTests(timelineResponse);
        }

        [TestMethod]
        public void HomeTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.HomeTimeline(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [TestMethod]
        public void RetweetsOfMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.RetweetsOfMe(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [TestMethod]
        public void RetweetedByMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.RetweetedByMe(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [TestMethod]
        public void RetweetedToMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.RetweetedToMe(tokens);

            PerformCommonTimelineTests(timelineResponse);
        }

        [TestMethod]
        public void Mentions()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.Mentions(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [TestMethod]
        public void SinceID()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.Mentions(tokens);

            PerformCommonTimelineTests(timelineResponse);

            decimal mostRecentId = timelineResponse.ResponseObject[0].Id;

            TwitterResponse<TwitterStatusCollection> timeline2 = TwitterTimeline.Mentions(tokens, new TimelineOptions
            {
                SinceStatusId = mostRecentId
            });

            PerformCommonTimelineTests(timeline2);
        }

        private static void PerformCommonTimelineTests(TwitterResponse<TwitterStatusCollection> timelineResponse)
        {
            Assert.IsNotNull(timelineResponse, "timelineResponse is null");
            Assert.IsTrue(timelineResponse.Result == RequestResult.Success, timelineResponse.ErrorMessage ?? timelineResponse.Result.ToString());
            Assert.IsNotNull(timelineResponse.ResponseObject, timelineResponse.ErrorMessage ?? timelineResponse.Result.ToString());
        }
    }
}
