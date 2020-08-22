using System;
using NUnit.Framework;
using Pyramid;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1, 1)]
        [TestCase(2, 3)]
        [TestCase(3, 6)]
        [TestCase(4, 10)]
        [TestCase(5, 15)]
        [TestCase(10, 55)]
        [TestCase(15, 120)]
        [TestCase(25, 325)]
        // [TestCase(100, 1)]
        public void GetNumberOfNodesTest_Pass(int numOfLayers, int expected)
        {
            Assert.AreEqual(expected, Program.GetNumberOfNodes(numOfLayers));
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void GetNumberOfNodesTest_ThrowException(int numOfLayers)
        {
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Number of layers too low"),
                delegate { Program.GetNumberOfNodes(numOfLayers); });
        }
    }
}