using System;

namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;

    [TestFixture]
    public static class TwitterTimelineTests
    {
        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void PublicTimeline()
        {
            TwitterStatusCollection timeline = TwitterTimeline.PublicTimeline().ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void UserTimeline()
        {
IAsyncResult asyncResult = TwitterTimelineAsync.UserTimeline(
    Configuration.GetTokens(),
    new UserTimelineOptions(),
    new TimeSpan(0, 2, 0),
    result =>
        {
            TwitterStatusCollection timeline = result.ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20,
                        "Timeline should contain between 0 and 20 items.");

            UserTimelineOptions User_Options = new UserTimelineOptions();
            User_Options.ScreenName = "twitterapi";
            User_Options.Count = 8;

            timeline = TwitterTimeline.UserTimeline(Configuration.GetTokens(), User_Options).ResponseObject;
            Assert.That(timeline.Count <= 8);

            timeline = TwitterTimeline.UserTimeline(User_Options).ResponseObject;
            Assert.That(timeline.Count <= 8);
        });

            asyncResult.AsyncWaitHandle.WaitOne();
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void FriendTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection timeline = TwitterTimeline.FriendTimeline(tokens).ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void RetweetsOfMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection timeline = TwitterTimeline.RetweetsOfMe(tokens).ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void RetweetedByMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection timeline = TwitterTimeline.RetweetedByMe(tokens).ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void RetweetedToMe()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection timeline = TwitterTimeline.RetweetedToMe(tokens).ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void Mentions()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection timeline = TwitterTimeline.Mentions(tokens).ResponseObject;
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }
    }
}
