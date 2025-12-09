using FizzBuzz.Parallel.Engine;

namespace FizzBuzz.Tests.ParallelEngineTests
{
    public class FizzBuzzParallelEngine_Should
    {
        private readonly FizzBuzzParallelEngine _parallelEngine = new();

        private readonly long end = 50000;


        public FizzBuzzParallelEngine_Should()
        {
            
        }

        [Fact]
        public async Task Return50000Items_ForRange1ToEnd()
        {
            // Act

            var fizzBuzzResponse = await _parallelEngine.GetRangeAsync(1, end);


            // Assert
            Assert.Equal(end, fizzBuzzResponse.FizzBuzzResults.Count());
        }
    }
}