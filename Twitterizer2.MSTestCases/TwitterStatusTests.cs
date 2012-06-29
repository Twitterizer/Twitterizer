using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer;
using Twitterizer.Entities;
using Twitterizer2.MSTestCases;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class TwitterStatusTests
    {
        [TestMethod]
        public void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterStatus.Show(tokens, 14772516348);
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
            Assert.IsNotNull(response.ResponseObject.Text, response.ErrorMessage);
            Assert.AreNotEqual(string.Empty, response.ResponseObject.Text, response.ErrorMessage);
        }

        [TestMethod]
        public void ShowMissing()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterStatus.Show(tokens, 1);
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
        }

        [TestMethod]
        public void UpdateAndDelete()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            StatusUpdateOptions options = new StatusUpdateOptions();

            var newStatus = TwitterStatus.Update(tokens, "Performing Twitterizer testing ...", options);
            Assert.IsNotNull(newStatus.ResponseObject, newStatus.ErrorMessage);
            Assert.IsTrue(newStatus.ResponseObject.Id > 0);

            var deletedStatus = newStatus.ResponseObject.Delete(tokens);
            Assert.IsNotNull(deletedStatus.ResponseObject, deletedStatus.ErrorMessage);
            Assert.IsTrue(newStatus.ResponseObject.Id == deletedStatus.ResponseObject.Id);
        }
        
        [TestMethod]
        [ExpectedException(exceptionType: typeof(ArgumentException))]
        public void TestTokenValidation()
        {
            OAuthTokens fakeTokens = new OAuthTokens();
            TwitterStatus.Update(fakeTokens, "This shouldn't work");
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(ArgumentException))]
        public void TestTokenValidation2()
        {
            OAuthTokens fakeTokens = new OAuthTokens
            {
                ConsumerKey = "fake",
                ConsumerSecret = "fake"
            };

            TwitterStatus.Update(fakeTokens, "This shouldn't work");
        }

        [TestMethod]
        public void UpdateWithURLParameters()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatus> response = TwitterStatus.Update(tokens, "This is a test. http://example.com/test?param=value");
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);

        }

        [TestMethod]
        public void UpdateWithPercentEnding()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatus> response = TwitterStatus.Update(tokens, "See this? This would break Hammock ... (#twitterizer wins) %%%");
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
        }

        [TestMethod]
        public void RelatedTweets()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterRelatedTweetsCollection> response = TwitterStatus.RelatedResultsShow(tokens, 25166473830);

            Assert.IsNotNull(response, response.ErrorMessage);
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage);
            Assert.AreNotEqual(0, response.ResponseObject.Count, response.ErrorMessage);
        }

        [TestMethod]
        public void DetectEntities()
        {
            // Get the public timeline
            var timelineResult = TwitterTimeline.PublicTimeline();

            Assert.IsNotNull(timelineResult, "timelineResult is null");
            Assert.IsNotNull(timelineResult.ResponseObject, timelineResult.ErrorMessage);
            Assert.AreNotEqual(0, timelineResult.ResponseObject.Count, timelineResult.ErrorMessage);

            // Get the first tweet with entities
            TwitterStatus tweet = timelineResult.ResponseObject.Where(x => x.Entities != null && x.Entities.OfType<TwitterMentionEntity>().Count() > 0).First();
            
            // Make sure that we got the tweet successfully
            Assert.IsNotNull(tweet);

            // Get the hashtags
            //var hashTags = from entities in tweet.Entities.OfType<TwitterHashTagEntity>()
            //                select entities;

            // Get the mentions (@screenname within the text)
            var mentions = from entities in tweet.Entities.OfType<TwitterMentionEntity>()
                            select entities;

            // Get urls
            //var urls = from entities in tweet.Entities.OfType<TwitterUrlEntity>()
            //            select entities;

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

        [TestMethod]
        public void GroupFavorites()
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

            //var combinedList = from t in request1.ResponseObject.Union(request2.ResponseObject)
            //                   group t by t.Id into g
            //                   select g.First();
        }

        [TestMethod]
        public void Conversation()
        {
            //OAuthTokens tokens = Configuration.GetTokens();

            var statusResponse = TwitterStatus.Show(7183041864671232);

            Assert.IsNotNull(statusResponse.ResponseObject, statusResponse.ErrorMessage);

            List<TwitterStatus> conversation = new List<TwitterStatus>();
            conversation.Add(statusResponse.ResponseObject);

            while (statusResponse.ResponseObject.InReplyToStatusId.HasValue)
            {
                statusResponse = TwitterStatus.Show(statusResponse.ResponseObject.InReplyToStatusId.Value);

                Assert.IsNotNull(statusResponse.ResponseObject, statusResponse.ErrorMessage);

                conversation.Insert(0, statusResponse.ResponseObject);
            }
        }
    }
}
