using MontyHallAPI.Controllers;
using NUnit.Framework;

namespace MontyTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            MontyHallController obj = new MontyHallController();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}