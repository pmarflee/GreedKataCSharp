using Xunit;

namespace GreedKata
{
    public class ScorerTests
    {
        [Theory]
        [InlineData(new[] { 1, 1, 1, 5, 1}, 1150)]
        [InlineData(new[] { 2, 3, 4, 6, 2}, 0)]
        [InlineData(new[] { 3, 4, 5, 3, 3}, 350)]
        [InlineData(new[] { 1, 5, 1, 2, 4}, 250)]
        [InlineData(new[] { 5, 5, 5, 5, 5}, 600)]
        public void Score(int[] dice, int expectedResult)
        {
            Assert.Equal(expectedResult, Scorer.Score(dice));
        }
    }
}
