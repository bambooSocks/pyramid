using System;
using System.Collections.Generic;
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

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(5, 3)]
        [TestCase(6, 4)]
        [TestCase(9, 4)]
        [TestCase(14, 5)]
        [TestCase(20, 6)]
        [TestCase(54, 10)]
        [TestCase(119, 15)]
        [TestCase(324, 25)]
        public void GetLayerFromNode_Pass(int node, int expected)
        {
            Assert.AreEqual(expected, Program.GetLayerFromNode(node));
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-5)]
        [TestCase(-10)]
        public void GetLayerFromNode_ThrowException(int node)
        {
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Invalid node"),
                delegate { Program.GetLayerFromNode(node); });
        }

        [Test]
        public void GenerateAdjacencyList_OneNode()
        {
            var expected = new List<List<int>> {null};
            Assert.AreEqual(expected, Program.GenerateAdjacencyList(1));
        }

        [Test]
        public void GenerateAdjacencyList_Simple()
        {
            var expected = new List<List<int>>();
            expected.Add(new List<int>{ 1,2 });
            expected.Add(new List<int>{ 3,4 });
            expected.Add(new List<int>{ 4,5 });
            expected.Add(null);
            expected.Add(null);
            expected.Add(null);
            
            Assert.AreEqual(expected, Program.GenerateAdjacencyList(3));
        }

        [Test]
        public void FindAllValidPaths_WithOutput()
        {
            var adjList = Program.GenerateAdjacencyList(4);
            var valueList = new List<int>{ 1, 8, 9, 1, 5, 9, 4, 5, 2, 3 };
            
            var expected = new List<List<int>>();
            expected.Add(new List<int>{ 1, 8, 1, 4 });
            expected.Add(new List<int>{ 1, 8, 5, 2 });
            Assert.AreEqual(expected, Program.FindAllValidPaths(0, adjList, valueList));
        }

        [Test]
        public void FindAllValidPaths_WithoutOutput()
        {
            var adjList = Program.GenerateAdjacencyList(4);
            var valueList = new List<int>{ 1, 8, 9, 1, 5, 9, 4, 5, 2, 3 };
            
            var expected = new List<List<int>>();
            Assert.AreEqual(expected, Program.FindAllValidPaths(2, adjList, valueList));
        }

        [Test]
        public void FindMaxSumPath1()
        {
            string pyramid = "1\n" +
                             "8 9\n" +
                             "1 5 9\n" +
                             "4 5 2 3";
            
            var expected = new List<int>{ 1, 8, 5, 2 };
            Assert.AreEqual(expected, Program.FindMaxSumPath(pyramid));
        }

        [Test]
        public void FindMaxSumPath2()
        {
            string pyramid = "215\n" +
                             "192 124\n" +
                             "117 269 442\n" +
                             "218 836 347 235\n" +
                             "320 805 522 417 345\n" +
                             "229 601 728 835 133 124\n" +
                             "248 202 277 433 207 263 257\n" +
                             "359 464 504 528 516 716 871 182\n" +
                             "461 441 426 656 863 560 380 171 923\n" +
                             "381 348 573 533 448 632 387 176 975 449\n" +
                             "223 711 445 645 245 543 931 532 937 541 444\n" +
                             "330 131 333 928 376 733 017 778 839 168 197 197\n" +
                             "131 171 522 137 217 224 291 413 528 520 227 229 928\n" +
                             "223 626 034 683 839 052 627 310 713 999 629 817 410 121\n" +
                             "924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";
            
            var expected = new List<int>{ 215, 192, 269, 836, 805, 728, 433, 528, 863, 632, 931, 778, 413, 310, 253 };
            Assert.AreEqual(expected, Program.FindMaxSumPath(pyramid));
        }
        
    }
}