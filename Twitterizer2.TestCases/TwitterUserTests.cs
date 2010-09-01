namespace Twitterizer2.TestCases
{
    using NUnit.Framework;
    using Twitterizer;
    using System;

    [TestFixture]
    public class TwitterUserTests
    {
        [Category("Read-Only")]
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

        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void Search()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUserCollection results = TwitterUser.Search(tokens, "twit_er_izer").ResponseObject;
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        [Category("Read-Write")]
        [Category("REST")]
        [Test]
        [TestCase]
        public static void UploadProfileImage()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterImage newProfileImage = TwitterImage.ReadFromDisk("Paper_Cup.jpg");
            TwitterUser updatedUser = TwitterUser.UpdateProfileImage(tokens, newProfileImage, null).ResponseObject;

            Assert.IsNotNull(updatedUser);
        }

        [Category("Read-Only")]
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

            // Second example, uses a callback method
            IAsyncResult asyncResult2 = TwitterUserAsync.Show(null, "twitterapi", null, new TimeSpan(0, 0, 5, 0), ShowUserCompleted);

            // Block the current thread until the other threads are completed
            asyncResult.AsyncWaitHandle.WaitOne();
            asyncResult2.AsyncWaitHandle.WaitOne();
        }

        private static void ShowUserCompleted(TwitterResponse<TwitterUser> user)
        {
            Console.WriteLine(user.ResponseObject.Status.Text);
        }
    }
}
