using Xunit;

namespace GreedKata
{
    public class GameTests
    {
        [Theory]
        [InlineData(new[] { 1, 1, 1, 5, 1 }, 1150)]
        [InlineData(new[] { 2, 3, 4, 6, 2 }, 0)]
        [InlineData(new[] { 3, 4, 5, 3, 3 }, 350)]
        [InlineData(new[] { 1, 5, 1, 2, 4 }, 250)]
        [InlineData(new[] { 5, 5, 5, 5, 5 }, 2000)]
        [InlineData(new[] { 2, 2, 2, 2, 5 }, 450)]
        [InlineData(new[] { 2, 2, 2, 2, 2 }, 800)]
        [InlineData(new[] { 2, 2, 2, 2, 2, 2 }, 1600)]
        public void Score(int[] dice, int expectedResult)
        {
            Assert.Equal(expectedResult, Game.Score(dice));
        }
    }
}
