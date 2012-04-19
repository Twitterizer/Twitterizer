using System;

namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;

    [TestFixture]
    public class TwitterTimelineTests
    {
        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void PublicTimeline()
        {
            TwitterStatusCollection timeline = TwitterTimeline.PublicTimeline().ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void UserTimeline()
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

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void FriendTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

#pragma warning disable 618
            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.FriendTimeline(tokens);
#pragma warning restore 618
            PerformCommonTimelineTests(timelineResponse);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void HomeTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.HomeTimeline(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void RetweetsOfMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.RetweetsOfMe(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void RetweetedByMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.RetweetedByMe(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void RetweetedToMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.RetweetedToMe(tokens);

            PerformCommonTimelineTests(timelineResponse);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void Mentions()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.Mentions(tokens);
            PerformCommonTimelineTests(timelineResponse);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void SinceID()
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
            Assert.IsNotNull(timelineResponse);
            Assert.That(timelineResponse.Result == RequestResult.Success, timelineResponse.ErrorMessage);
            Assert.IsNotNull(timelineResponse.ResponseObject);
        }
    }
}
