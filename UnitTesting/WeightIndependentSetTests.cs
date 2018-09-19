using Algorithms.Stanford.Graphs;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public class WeightIndependentSetTests
    {
        [Test]
        public void SmallPathGraph()
        {
            var graph = new[] {1, 4, 5, 4};
            var nodesShouldBeIncluded = new []{2, 4};
            var nodesShouldNotBeIncluded = new[] {1, 3};
            var maxSetFinder = new MaxWeightIndependentSetPathGraph();
            var result = maxSetFinder.GetMaxWeightIndependentSet(graph);

            for (int i = 0; i < nodesShouldBeIncluded.Length; i++) 
            {
                Assert.IsTrue(result.Contains(nodesShouldBeIncluded[i]));
                Assert.IsFalse(result.Contains(nodesShouldNotBeIncluded[i]));
            }
        }
    }
}