using FizzBuzz.Engine;

namespace FizzBuzz.Tests.EngineTests
{
    public class FizzBuzzEngine_Should : IClassFixture<FizzBuzzEngine_Fixture>
    {
        private readonly FizzBuzzEngine _engine;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        public FizzBuzzEngine_Should(FizzBuzzEngine_Fixture fixture)
        {
            _engine = fixture.Engine;
        }

        [Fact]
        public async Task ReturnFailureWhen_CallingForRangeAnd_EndGreaterThanStart() 
        {
            // Arrange
            int start = 10;
            int end = 5;

            // Act
            var fizzBuzzResponse = await _engine.GenerateFizzBuzzForRange(start, end, _cancellationToken);

            // Assert
            Assert.False(fizzBuzzResponse.Success);
        }

        [Fact]
        public async Task ReturnFailureWhen_CallingForRangeReverseAnd_StartGreaterThanEnd()
        {
            // Arrange
            int start = 5;
            int end = 10;

            // Act
            var fizzBuzzResponse = await _engine.GenerateFizzBuzzForRangeReverse(start, end, _cancellationToken);

            // Assert
            Assert.False(fizzBuzzResponse.Success);
        }

        [Fact]
        public async Task ReturnValues_ForRange1To5()
        {
            // Act
            var fizzBuzzResponse = await _engine.GenerateFizzBuzzForRange(1, 5, _cancellationToken);

            // Assert
            Assert.Equal(new[] { "1", "2", Consts.Fizz, "4", Consts.Buzz }, fizzBuzzResponse.FizzBuzzResults);
        }

        [Fact]
        public async Task ReturnValues_ForRangeReverse10To5()
        {
            // Act
            var fizzBuzzResponse = await _engine.GenerateFizzBuzzForRangeReverse(10, 5, _cancellationToken);

            // Assert
            Assert.Equal(new[] { Consts.Buzz, Consts.Fizz, "8", "7", Consts.Fizz, Consts.Buzz }, fizzBuzzResponse.FizzBuzzResults);
        }

        [Theory]
        [InlineData(3, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(7, "7")]
        public async Task ReturnValues_ForSingle(int input, string expected)
        {
            // Act
            var actual = await _engine.GenerateFizzBuzzForSingle(input, _cancellationToken);

            // Assert
            Assert.Equal(expected, actual.FizzBuzzResults[0]);
        }
    }
}