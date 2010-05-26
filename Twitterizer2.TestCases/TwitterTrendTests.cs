namespace Twitterizer2.TestCases
{
    using Twitterizer;
    using NUnit.Framework;
    
    [TestFixture]
    public static class TwitterTrendTests
    {
        [Test]
        public static void CurrentTrends()
        {
            TwitterTrendCollection trends = TwitterTrend.Current();
            Assert.IsNotNull(trends);
        }
    }
}
