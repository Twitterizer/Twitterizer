using System;
using NUnit.Framework;
using Twitterizer;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class OAuthTests
    {
        [Test]
        public static void RequestToken()
        {
            //OAuthTokens tokens = Configuration.GetTokens();

            //OAuthTokenResponse response = OAuthUtility.GetRequestToken(tokens.ConsumerKey, tokens.ConsumerSecret, "oob");

            //Assert.IsNotNull(response);
            //Assert.IsNotNullOrEmpty(response.Token);
        }

        [Test]
        public static void UrlInStatus()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterStatus> result = TwitterStatus.Update(tokens, "I am testing. Please ignore this. protocol://host/page?key=value&key2=value2" + new Random().Next(1000).ToString());
            Assert.IsNotNull(result.ResponseObject);
            Assert.That(result.ResponseObject.Id > 0);
        }

        [Test]
        public static void WebRequestBuilderParts()
        {
            WebRequestBuilder builder = new WebRequestBuilder(
                new Uri("http://example.com/endpoint"),
                HTTPVerb.GET,
                new OAuthTokens
                                {
                                    ConsumerKey =  "key",
                                    ConsumerSecret = "518F1B7B4C2F855EFF3DEFDAF1311",
                                    AccessToken = "token",
                                    AccessTokenSecret = "A6FE4462BAA8C3ADEA7D9E3BCD5BB"
                                }
            );

            // Manually add our values
            builder.Parameters.Add("oauth_version", "1.0");
            builder.Parameters.Add("oauth_nonce", "5565373");
            builder.Parameters.Add("oauth_timestamp", "1283303184");
            builder.Parameters.Add("oauth_signature_method", "HMAC-SHA1");
            builder.Parameters.Add("oauth_consumer_key", "49A8746B34B83CA13D1B9DACFD251");
            builder.Parameters.Add("oauth_consumer_secret", "518F1B7B4C2F855EFF3DEFDAF1311");
            builder.Parameters.Add("oauth_token", "8B163EC93574682CFF3FD6B45BD55");
            builder.Parameters.Add("oauth_token_secret", "A6FE4462BAA8C3ADEA7D9E3BCD5BB");

            string signature = builder.GenerateSignature();

            Assert.AreEqual(signature, "ndQStX289rIMyZR5dErSinLK/bQ=");

            builder.Parameters.Add("text", "this is a test http://example.com/test?key=value&key%202");
            signature = builder.GenerateSignature();
            Assert.AreEqual(signature, "MUUsZ/iV/FXxBRPdyxRkNKqEShw=");

            builder.Verb = HTTPVerb.POST;
            signature = builder.GenerateSignature();
            Assert.AreEqual(signature, "wtbXMN5BwtH8r2/G/Rwqdp7HMnU=");
        }
    }
}
