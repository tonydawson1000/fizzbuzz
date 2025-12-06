using FizzBuzz.Engine;

namespace FizzBuzz.Tests
{
    public class FizzBuzzEngine_Should : IClassFixture<FizzBuzzEngine_Fixture>
    {
        private readonly FizzBuzzEngine _engine;

        public FizzBuzzEngine_Should(FizzBuzzEngine_Fixture fixture)
        {
            _engine = fixture.Engine;
        }

        [Fact]
        public void ThrowExceptionWhen_EndGreaterThanStart() 
        {
            // Arrange
            int start = 10;
            int end = 5;

            // Act
            var ex = Assert.Throws<ArgumentException>(() => _engine.GenerateFizzBuzzForRange(start, end).ToList());

            // Assert
            Assert.Equal(Consts.ErrorStartGreaterThanEnd, ex.Message);
        }

        [Fact]
        public void ReturnValues_ForRange1To5()
        {
            var results = _engine.GenerateFizzBuzzForRange(1, 5).ToList();

            Assert.Equal(new[] { "1", "2", Consts.Fizz, "4", Consts.Buzz }, results);
        }

        [Theory]
        [InlineData(3, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(7, "7")]
        public void ReturnValues_ForSingle(int input, string expected)
        {
            Assert.Equal(expected, _engine.GenerateFizzBuzzForSingle(input));
        }
    }
}