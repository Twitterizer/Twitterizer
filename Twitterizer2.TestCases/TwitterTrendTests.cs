namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;
    
    [TestFixture]
    public static class TwitterTrendTests
    {
        [Category("Read-Only")]
        [Category("Search")]
        [Test]
        public static void CurrentTrends()
        {
            TwitterTrendCollection trends = TwitterTrend.Current().ResponseObject;
            Assert.IsNotNull(trends);
        }
    }
}
