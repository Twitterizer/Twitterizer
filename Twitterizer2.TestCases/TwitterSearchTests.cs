namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;

    [TestFixture]
    public class TwitterSearchTests
    {
        [Test]
        [Category("Read-Only")]
        [Category("Search")]
        public static void Search()
        {
            SearchOptions options = new SearchOptions();
            options.NumberPerPage = 19;

            TwitterSearchResultCollection results = TwitterSearch.Search("from:twit_er_izer OR twitterizer OR @twit_er_izer", options);

            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.That(results.Count == 19);
        }

        [Test]
        [Category("Read-Only")]
        [Category("Search")]
        public static void SearchLocal()
        {
            SearchOptions options = new SearchOptions();
            options.GeoCode = "30.4413,-84.2809,20mi";

            TwitterSearchResultCollection results = TwitterSearch.Search("tallahassee", options);

            Assert.False(results.IsEmpty != false);
        }
    }
}
