namespace Twitterizer2.MSTestCases
{
    using Twitterizer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    [TestClass]
    public class TwitterTrendTests
    {
        [TestMethod]
        public void CurrentTrends()
        {
            var result = TwitterTrend.Trends(1);
            Assert.IsNotNull(result.ResponseObject, result.ErrorMessage);
        }
    }
}
