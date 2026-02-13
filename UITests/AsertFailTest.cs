using System;


namespace NunitPlayV2.UITests
{
    public class AsertFailTest
    {

        [Test]
        public void AssertTest1()
        {
            Assert.Fail();
        }

        [Test]
        public void AssertTest2()
        {
            Assert.Pass();
        }
    }
}
