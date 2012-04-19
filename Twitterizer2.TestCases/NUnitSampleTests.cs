using NUnit.Framework;

namespace Twitterizer2.TestCases
{
    [TestFixture]
    public class NUnitSampleTests
    {
        [Test]
        public void SomeFailingTest()
        {
            Assert.Greater(5, 7);
        }

        [Test]
        public void SomePassingTest()
        {
            Assert.AreEqual(5, 5);
        }
    }
}