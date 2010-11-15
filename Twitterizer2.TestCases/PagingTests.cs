namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;
    
    [TestFixture()]
    public class PagingTests
    {
        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Mentions()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection results = TwitterTimeline.Mentions(tokens).ResponseObject;
            
            int pagenumber = 1;
            string firstStatusText = results[0].Text;

            while (results != null && results.Count > 0)
            {
                Assert.IsNotEmpty(results);

                if (pagenumber > 1)
                    Assert.That(results[0].Text != firstStatusText);

                results = results.NextPage().ResponseObject;

                pagenumber++;
            }
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void UserTimeline()
        {
            OAuthTokens tokens = Configuration.GetTokens();
            int pagenumber = 1;

            TwitterResponse<TwitterStatusCollection> results = TwitterTimeline.UserTimeline(tokens,
                                                                                            new UserTimelineOptions()
                                                                                                {Count = 100});

            string firstStatusText = results.ResponseObject[0].Text;

            while (results.ResponseObject != null && results.ResponseObject.Count > 0 && results.RateLimiting.Remaining > 0)
            {
                Assert.IsNotEmpty(results.ResponseObject);

                if (pagenumber > 1)
                    Assert.That(results.ResponseObject[0].Text != firstStatusText);

                results = results.ResponseObject.NextPage();

                pagenumber++;
            }
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Friends()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatusCollection> timelineResponse = TwitterTimeline.FriendTimeline(tokens);
            Assert.IsNotNull(timelineResponse);
            Assert.That(timelineResponse.Result == RequestResult.Success);

            int pagenumber = 1;
            string firstStatusText = timelineResponse.ResponseObject[0].Text;

            while (timelineResponse.ResponseObject != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(timelineResponse.ResponseObject);

                if (pagenumber > 1)
                    Assert.That(timelineResponse.ResponseObject[0].Text != firstStatusText);

                timelineResponse = timelineResponse.ResponseObject.NextPage();
                Assert.IsNotNull(timelineResponse);
                Assert.That(timelineResponse.Result == RequestResult.Success);

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Home()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatusCollection results = TwitterTimeline.HomeTimeline(tokens).ResponseObject;

            int pagenumber = 1;
            string firstStatusText = results[0].Text;

            while (results != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(results);

                if (pagenumber > 1)
                    Assert.That(results[0].Text != firstStatusText);

                results = results.NextPage().ResponseObject;

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void Followers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUserCollection followers = TwitterFriendship.Followers(tokens, new FollowersOptions()
            {
                ScreenName = "twitterapi"
            }).ResponseObject;

            int pagenumber = 1;
            decimal firstId = followers[0].Id;

            while (followers != null && pagenumber < 3)
            {
                Assert.IsNotEmpty(followers);

                if (pagenumber > 1)
                    Assert.That(followers[0].Id != firstId);

                followers = followers.NextPage().ResponseObject;

                pagenumber++;
            }

            Assert.That(pagenumber > 1);
        }

        [Test]
        [Category("Read-Only")]
        [Category("REST")]
        [Category("Paging")]
        public static void FollowersIds()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<UserIdCollection> response = TwitterFriendship.FollowersIds(tokens, new UsersIdsOptions()
            {
                ScreenName = "twitterapi"
            });

            Assert.IsNotNull(response);
            Assert.That(response.Result == RequestResult.Success);
            Assert.IsNotNull(response.ResponseObject);
            Assert.That(response.ResponseObject.Count > 0);

            decimal firstId = response.ResponseObject[0];

            response = response.ResponseObject.NextPage();
            Assert.IsNotNull(response);
            Assert.That(response.Result == RequestResult.Success);
            Assert.IsNotNull(response.ResponseObject);
            Assert.That(response.ResponseObject.Count > 0);
            Assert.AreNotEqual(response.ResponseObject[0], firstId);
        }
    }
}
