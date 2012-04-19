namespace Twitterizer2.TestCases
{
    using NUnit.Framework;
    using Twitterizer;
    using System;

    [TestFixture]
    public class TwitterUserTests
    {
        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUser user = TwitterUser.Show(tokens, "twit_er_izer").ResponseObject;
            Assert.IsNotNull(user);
            Assert.That(!string.IsNullOrEmpty(user.ScreenName));

            Assert.IsNotNull(user.Status);
            Assert.That(!string.IsNullOrEmpty(user.Status.Text));
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void Search()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUserCollection results = TwitterUser.Search(tokens, "twit_er_izer").ResponseObject;
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void LookupUsers()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            LookupUsersOptions options = new LookupUsersOptions();
            options.ScreenNames.Add("twitterapi");
            options.ScreenNames.Add("digitallyborn");
            options.ScreenNames.Add("trixtur");
            options.ScreenNames.Add("twit_er_izer");

            var result = TwitterUser.Lookup(tokens, options);

            Assert.That(result.Result == RequestResult.Success);
            Assert.IsNotNull(result.ResponseObject);
        }

        [Category("ReadOnly")]
        [Category("REST")]
        [Test]
        public static void LookupUsersById()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterIdCollection userIds = new TwitterIdCollection
                                              {
                                                  14725805, // digitallyborn
                                                  16144513, // twit_er_izer
                                                  6253282 // twitterapi
                                              };
            
            var result = TwitterUser.Lookup(tokens, new LookupUsersOptions { UserIds = userIds });
            
            Assert.That(result.Result == RequestResult.Success);
            Assert.IsNotNull(result.ResponseObject);
        }

        [Test]
        public static void AsyncTest()
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

        [Test]
        public static void AsyncTest2()
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

        [Test]
        public static void RetweetedBy()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var response = TwitterUser.RetweetedBy(tokens, 5053218102972417);

            Assert.IsNotNull(response);
            Assert.That(response.Result == RequestResult.Success);
            Assert.IsNotNull(response.ResponseObject);
            Assert.IsNotEmpty(response.ResponseObject);
        }
    }
}
