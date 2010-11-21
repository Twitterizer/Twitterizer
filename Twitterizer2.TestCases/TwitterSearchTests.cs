namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;

    [TestFixture]
    public class TwitterSearchTests
    {
        [Test]
        [Category("ReadOnly")]
        [Category("Search")]
        public static void Search()
        {
            SearchOptions options = new SearchOptions();
            options.NumberPerPage = 19;

            TwitterResponse<TwitterSearchResultCollection> searchResponse = TwitterSearch.Search("twitter", options);

            Assert.IsNotNull(searchResponse);
            Assert.That(searchResponse.Result == RequestResult.Success, searchResponse.ErrorMessage);
            Assert.IsNotNull(searchResponse.ResponseObject);
            Assert.That(searchResponse.ResponseObject.Count == 19);

            var request = Twitterizer.TwitterSearch.Search("twitter");
            Assert.IsNotNull(request);
            Assert.That(request.Result == RequestResult.Success, request.ErrorMessage);
            Assert.IsNotNull(request.ResponseObject);

        }

        [Test]
        [Category("ReadOnly")]
        [Category("Search")]
        public static void SearchLocal()
        {
            SearchOptions options = new SearchOptions();
            options.GeoCode = "30.4413,-84.2809,20mi";

            TwitterResponse<TwitterSearchResultCollection> searchResponse = TwitterSearch.Search("", options);

            Assert.IsNotNull(searchResponse);
            Assert.That(searchResponse.Result == RequestResult.Success, searchResponse.ErrorMessage);
            Assert.IsNotNull(searchResponse.ResponseObject);
        }
    }
}
