namespace Twitterizer2.MSTestCases
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Twitterizer;

    [TestClass]
    public class TwitterGeoTests
    {
        [TestMethod]
        public void LookupPlaces()
        {
            TwitterPlaceLookupOptions options = new TwitterPlaceLookupOptions
            {
                Granularity = "city",
                MaxResults = 2
            };

            var places = TwitterPlace.Lookup(30.475012, -84.35509, options);

            Assert.IsNotNull(places.ResponseObject, places.ErrorMessage);
            Assert.AreNotEqual(0, places.ResponseObject.Count, places.ErrorMessage);
            Assert.IsTrue(places.ResponseObject.Count == 2, places.ErrorMessage);
        }
    }
}
