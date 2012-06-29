namespace Twitterizer2.MSTestCases
{
    using Twitterizer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TwitterSearchTests
    {
        [TestMethod]
        public void Search()
        {
            SearchOptions options = new SearchOptions();
            options.NumberPerPage = 19;

            TwitterResponse<TwitterSearchResultCollection> searchResponse = TwitterSearch.Search("twitter", options);

            Assert.IsNotNull(searchResponse, "searchResponse is null");
            Assert.IsTrue(searchResponse.Result == RequestResult.Success, searchResponse.ErrorMessage);
            Assert.IsNotNull(searchResponse.ResponseObject, searchResponse.ErrorMessage);
            Assert.IsTrue(searchResponse.ResponseObject.Count == 19);

            var request = Twitterizer.TwitterSearch.Search("twitter");
            Assert.IsNotNull(request, "request is null");
            Assert.IsTrue(request.Result == RequestResult.Success, request.ErrorMessage);
            Assert.IsNotNull(request.ResponseObject, request.ErrorMessage);

        }

        [TestMethod]
        public void SearchLocal()
        {
            SearchOptions options = new SearchOptions();
            options.GeoCode = "30.4413,-84.2809,20mi";

            TwitterResponse<TwitterSearchResultCollection> searchResponse = TwitterSearch.Search("twitter", options);

            Assert.IsNotNull(searchResponse, "searchResponse is null");
            Assert.IsTrue(searchResponse.Result == RequestResult.Success, searchResponse.ErrorMessage);
            Assert.IsNotNull(searchResponse.ResponseObject, searchResponse.ErrorMessage);
        }
    }
}
