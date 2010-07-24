namespace Twitterizer2.TestCases
{
    using NUnit.Framework;
    using Twitterizer;
    
    [TestFixture]
    public class TwitterUserTests
    {
        [Category("Read-Only")]
        [Category("REST")]
        [Test]
        public static void Show()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterUser user = TwitterUser.Show(tokens, "twit_er_izer");
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

            TwitterUserCollection results = TwitterUser.Search(tokens, "twit_er_izer");
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
            TwitterUser updatedUser = TwitterUser.UpdateProfileImage(tokens, newProfileImage, null);

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

            TwitterUserCollection users = TwitterUser.Lookup(tokens, options);
        }
    }
}
