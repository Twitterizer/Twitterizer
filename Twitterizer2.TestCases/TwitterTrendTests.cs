namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;
    
    [TestFixture]
    public class TwitterTrendTests
    {
        [Category("ReadOnly")]
        [Category("Search")]
        [Test]
        public static void CurrentTrends()
        {
            TwitterTrendCollection trends = TwitterTrend.Trends(1).ResponseObject;
            Assert.IsNotNull(trends);
        }
    }
}
