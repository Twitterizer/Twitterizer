using NUnit.Framework;
using Twitterizer;
using System.Linq;
using System.Xml.Linq;

namespace Twitterizer2.TestCases
{
    [TestFixture]    
    public class Issue59
    {
        [Test]
        public static void RunTest()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            TwitterResponse<TwitterUser> firstUserResponse = TwitterUser.Show(tokens, "twit_er_izer");
            Assert.IsNotNull(firstUserResponse);
            Assert.That(firstUserResponse.Result == RequestResult.Success);

            TwitterResponse<TwitterUser> secondUserResponse = TwitterUser.Show(tokens, "twitterapi");
            Assert.IsNotNull(secondUserResponse);
            Assert.That(secondUserResponse.Result == RequestResult.Success);

            // This is the query, modified as little as possible.
            var query = from list in TwitterList.GetLists(tokens, firstUserResponse.ResponseObject.ScreenName).ResponseObject
                        select new XElement("input", new XAttribute("type", "checkbox"),
                            TwitterList.CheckMembership(
                                tokens,
                                firstUserResponse.ResponseObject.ScreenName,
                                list.Id.ToString(),
                                secondUserResponse.ResponseObject.Id).ResponseObject == null ? null : new XAttribute("checked", "")
                        );
        }
    }
}
