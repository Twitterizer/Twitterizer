namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;
    using System.Globalization;

    [TestFixture]
    public static class TwitterTimelineTests
    {
        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void PublicTimeline()
        {
            TwitterStatusCollection timeline = TwitterTimeline.PublicTimeline();
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void UserTimeline()
        {
            TwitterStatusCollection timeline = TwitterTimeline.UserTimeline(Configuration.GetTokens());
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void UserTimelineUnauthorized()
        {
            UserTimelineOptions options = new UserTimelineOptions()
            {
                ScreenName = "twitterapi"
            };

            TwitterStatusCollection timeline = TwitterTimeline.UserTimeline(options);
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void FriendTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection timeline = TwitterTimeline.FriendTimeline(tokens);
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

            TwitterStatusCollection timeline = TwitterTimeline.RetweetsOfMe(tokens);
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

            TwitterStatusCollection timeline = TwitterTimeline.RetweetedByMe(tokens);
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

            TwitterStatusCollection timeline = TwitterTimeline.RetweetedToMe(tokens);
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

            TwitterStatusCollection timeline = TwitterTimeline.Mentions(tokens);
            Assert.IsNotNull(timeline);
            Assert.IsNotEmpty(timeline);

            Assert.That(timeline.Count > 0 && timeline.Count <= 20, "Timeline should contain between 0 and 20 items.");
        }
    }
}
