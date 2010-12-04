using System;
using NUnit.Framework;

using System.Linq;
using Twitterizer;
using Twitterizer.Entities;

using System.Diagnostics;
using System.Collections.Generic;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public static class TwitterStatusTests
    {
        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatus> response = TwitterStatus.Show(tokens, 14772516348);
            TwitterStatus status = response.ResponseObject;
            Assert.IsNotNull(status);
            Assert.IsNotNullOrEmpty(status.Text);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void ShowMissing()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatus missingStatus = TwitterStatus.Show(tokens, 1).ResponseObject;
            Assert.IsNotNull(missingStatus);
        }

        [Category("Read-Write")]
        [Category("REST")]
        [Test]
        public static void UpdateAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            StatusUpdateOptions options = new StatusUpdateOptions();

            TwitterStatus newStatus = TwitterStatus.Update(tokens, "Performing Twitterizer testing ...", options).ResponseObject;
            Assert.That(newStatus.Id > 0);

            TwitterStatus deletedStatus = newStatus.Delete(tokens).ResponseObject;
            Assert.That(newStatus.Id == deletedStatus.Id);
        }
        
        [Category("Core")]
        [Test]
        [ExpectedException(ExpectedException=typeof(ArgumentException))]
        public static void TestTokenValidation()
        {
            OAuthTokens fakeTokens = new OAuthTokens();
            TwitterStatus.Update(fakeTokens, "This shouldn't work");
        }

        [Category("Core")]
        [Test]
        [ExpectedException(ExpectedException = typeof(ArgumentException))]
        public static void TestTokenValidation2()
        {
            OAuthTokens fakeTokens = new OAuthTokens()
                {
                    ConsumerKey = "fake",
                    ConsumerSecret = "fake"
                };

            TwitterStatus.Update(fakeTokens, "This shouldn't work");
        }

        [Test]
        public static void UpdateWithURLParameters()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatus> response = TwitterStatus.Update(tokens, "This is a test. http://example.com/test?param=value");
            Assert.IsNotNull(response.ResponseObject);

        }

        [Test]
        public static void UpdateWithPercentEnding()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatus> response = TwitterStatus.Update(tokens, "See this? This would break Hammock ... (#twitterizer wins) %%%");
            Assert.IsNotNull(response.ResponseObject);
        }

        [Test]
        public static void RelatedTweets()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterRelatedTweetsCollection> response =
                TwitterStatus.RelatedResultsShow(tokens, 25166473830);

            Assert.IsNotNull(response);
            Assert.That(response.Result == RequestResult.Success);
            Assert.IsNotEmpty(response.ResponseObject);
        }

        [Test]
        public static void DetectEntities()
        {
            // Get the public timeline
            TwitterResponse<TwitterStatusCollection> timelineResult = TwitterTimeline.PublicTimeline();

            // Get the first tweet with entities
            TwitterStatus tweet = timelineResult.ResponseObject.Where(x => x.Entities != null && x.Entities.OfType<TwitterMentionEntity>().Count() > 0).First();
            
            // Make sure that we got the tweet successfully
            Assert.IsNotNull(tweet);

            // Get the hashtags
            var hashTags = from entities in tweet.Entities.OfType<TwitterHashTagEntity>()
                            select entities;

            // Get the mentions (@screenname within the text)
            var mentions = from entities in tweet.Entities.OfType<TwitterMentionEntity>()
                            select entities;

            // Get urls
            var urls = from entities in tweet.Entities.OfType<TwitterUrlEntity>()
                        select entities;

            // Get the tweet text to be modified
            string modifiedTweetText = tweet.Text;

            // Loop through the mentions from the back of the text to the beginning (to retain the index)
            foreach (TwitterMentionEntity mention in mentions.OrderBy(m => m.StartIndex).Reverse())
            {
                string linkText = string.Format("<a href=\"http://twitter.com/{0}\">@{0}</a>", mention.ScreenName);

                modifiedTweetText = string.Concat(
                    modifiedTweetText.Substring(0, mention.StartIndex),
                    linkText,
                    modifiedTweetText.Substring(mention.EndIndex));
            }
        }

        [Test]
        public static void GroupFavorites()
        {
            // Get the first list, from @twit_er_izer
            TwitterResponse<TwitterStatusCollection> request1 = TwitterFavorite.List(new ListFavoritesOptions
            {
                UserNameOrId = "twit_er_izer"
            });

            // Get the second list, from @mschilling
            TwitterResponse<TwitterStatusCollection> request2 = TwitterFavorite.List(new ListFavoritesOptions
            {
                UserNameOrId = "mschilling"
            });

            var combinedList = from t in request1.ResponseObject.Union(request2.ResponseObject)
                               group t by t.Id into g
                               select g.First();
            
            
        }

        [Test]
        public static void Conversation()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var statusResponse = TwitterStatus.Show(7183041864671232);

            List<TwitterStatus> conversation = new List<TwitterStatus>();
            conversation.Add(statusResponse.ResponseObject);

            while (statusResponse != null && statusResponse.ResponseObject.InReplyToStatusId.HasValue)
            {
                statusResponse = TwitterStatus.Show(statusResponse.ResponseObject.InReplyToStatusId.Value);

                conversation.Insert(0, statusResponse.ResponseObject);
            }
        }
    }
}
