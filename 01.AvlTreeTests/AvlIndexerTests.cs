namespace AvlTreeTests
{
    using System;
    using System.Linq;
    using AvlTreeLab;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AvlIndexerTests
    {
        private int[] testNums;
        private AvlTree<int> avlTree;

        [TestInitialize]
        public void InitializeAvlTests()
        {
            this.avlTree = new AvlTree<int>();

            this.testNums = TestUtils.ToIntArray("20 30 5 8 14 18 -2 0 50 50");

            foreach (var num in this.testNums)
            {
                this.avlTree.Add(num);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Indexer_InvalidPositiveIndex_ShouldThrow()
        {
            var invalidIndex = this.avlTree[100];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Indexer_InvalidNegativeIndex_ShouldThrow()
        {
            var invalidIndex = this.avlTree[-100];
        }

        [TestMethod]
        public void Indexer_ValidIndices_ShouldReturnCorrectly()
        {
            var sorted = this.testNums.OrderBy(n => n);
            var index = 0;
            foreach (var num in sorted)
            {
                Assert.AreEqual(num, this.avlTree[index]);
                index++;
            }
        }
    }
}