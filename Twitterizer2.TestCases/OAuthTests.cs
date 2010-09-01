using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Twitterizer;
using System.Net;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class OAuthTests
    {
        [Test]
        public static void RequestToken()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            OAuthTokenResponse response = OAuthUtility.GetRequestToken(tokens.ConsumerKey, tokens.ConsumerSecret, string.Empty);

            Assert.IsNotNull(response);
            Assert.IsNotNullOrEmpty(response.Token);
        }

        [Test]
        public static void UrlInStatus()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterStatus result = TwitterStatus.Update(tokens, "I am testing. Please ignore this. protocol://host/page?key=value&key2=value2");
            Assert.IsNotNull(result);
            Assert.That(result.Id > 0);
        }

        [Test]
        public static void WebRequestBuilderParts()
        {
            WebRequestBuilder builder = new WebRequestBuilder(new Uri("http://example.com/endpoint"), HTTPVerb.GET)
                                            {
                                                Tokens = new OAuthTokens()
                                                             {
                                                                 ConsumerSecret = "518F1B7B4C2F855EFF3DEFDAF1311",
                                                                 AccessTokenSecret = "A6FE4462BAA8C3ADEA7D9E3BCD5BB"
                                                             }
                                            };

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

            builder.Parameters.Add("text", "this is a test http://example.com/test?key=value&key2");
            signature = builder.GenerateSignature();
            Assert.AreEqual(signature, "kJPoeB85sop1rkkyu9OJ/CWO5QE=");

            builder.Verb = HTTPVerb.POST;
            signature = builder.GenerateSignature();
            Assert.AreEqual(signature, "xBt9QMoM+wlC2RNoS4ZOOLLCMow=");
        }
    }
}
