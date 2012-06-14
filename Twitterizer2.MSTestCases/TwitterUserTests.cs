namespace Twitterizer2.MSTestCases
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Twitterizer;
    using System;
    using Twitterizer2.MSTestCases;

    [TestClass]
    public class TwitterUserTests
    {
        [TestMethod]
        public void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterUser.Show(tokens, "twit_er_izer");
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
            Assert.IsTrue(!string.IsNullOrEmpty(response.ResponseObject.ScreenName), response.ErrorMessage);

            Assert.IsNotNull(response.ResponseObject.Status, response.ErrorMessage);
            Assert.IsTrue(!string.IsNullOrEmpty(response.ResponseObject.Status.Text), response.ErrorMessage);
        }

        [TestMethod]
        public void Search()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterUser.Search(tokens, "twit_er_izer");
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
            Assert.AreNotEqual(0, response.ResponseObject.Count, response.ErrorMessage);
        }

        [TestMethod]
        public void LookupUsers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            LookupUsersOptions options = new LookupUsersOptions();
            options.ScreenNames.Add("twitterapi");
            options.ScreenNames.Add("digitallyborn");
            options.ScreenNames.Add("trixtur");
            options.ScreenNames.Add("twit_er_izer");

            var result = TwitterUser.Lookup(tokens, options);

            Assert.IsTrue(result.Result == RequestResult.Success, result.ErrorMessage);
            Assert.IsNotNull(result.ResponseObject, result.ErrorMessage);
        }

        [TestMethod]
        public void LookupUsersById()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterIdCollection userIds = new TwitterIdCollection
                                              {
                                                  14725805, // digitallyborn
                                                  16144513, // twit_er_izer
                                                  6253282 // twitterapi
                                              };
            
            var result = TwitterUser.Lookup(tokens, new LookupUsersOptions { UserIds = userIds });

            Assert.IsTrue(result.Result == RequestResult.Success, result.ErrorMessage);
            Assert.IsNotNull(result.ResponseObject, result.ErrorMessage);
        }

        [TestMethod]
        public void AsyncTest()
        {
            // First example, uses lambda expression
            IAsyncResult asyncResult = TwitterUserAsync.Show(
                null,           // tokens
                "twit_er_izer",     // screen_name
                null,           // optional parameters
                new TimeSpan(0, 0, 5, 0), // async timeout
                response => Console.WriteLine(response.ResponseObject.Status.Text));

            asyncResult.AsyncWaitHandle.WaitOne();
        }

        [TestMethod]
        public void AsyncTest2()
        {
            // Second example, uses a callback method
            IAsyncResult asyncResult = TwitterUserAsync.Show(null, "twitterapi", null, new TimeSpan(0, 0, 5, 0), ShowUserCompleted);

            // Block the current thread until the other threads are completed
            asyncResult.AsyncWaitHandle.WaitOne();
            
        }

        private static void ShowUserCompleted(TwitterResponse<TwitterUser> user)
        {
            Console.WriteLine(user.ResponseObject.Status.Text);
        }

        [TestMethod]
        public void RetweetedBy()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterUser.RetweetedBy(tokens, 5053218102972417);

            Assert.IsNotNull(response, response.ErrorMessage);
            Assert.IsTrue(response.Result == RequestResult.Success, response.ErrorMessage);
            Assert.IsNotNull(response.ResponseObject, response.ErrorMessage);
            Assert.AreNotEqual(0, response.ResponseObject.Count, response.ErrorMessage);
        }
    }
}
