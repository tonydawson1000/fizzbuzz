using FizzBuzz.Engine;
using FizzBuzz.Rule.Engine;

namespace FizzBuzz.Tests.RuleEngineTests
{
    public class FizzBuzzRuleEngine_Should : IClassFixture<FizzBuzzRuleEngine_Fixture>
    {
        private readonly FizzBuzzRuleEngine _ruleEngine;

        public FizzBuzzRuleEngine_Should(FizzBuzzRuleEngine_Fixture fixture)
        {
            _ruleEngine = fixture.RuleEngine;
        }

        [Fact]
        public void ReturnValues_ForRange1To5()
        {
            // Act
            var fizzBuzzResponse = _ruleEngine.Run(1, 5);

            // Assert
            Assert.Equal(new[] { "1", "2", Consts.Fizz, "4", Consts.Buzz }, fizzBuzzResponse);
        }

        [Fact]
        public void Return100Items_ForRange1To100()
        {
            // Act
            var fizzBuzzResponse = _ruleEngine.Run(1, 100);

            // Added for Debug only
            foreach (var item in fizzBuzzResponse)
            {
                Console.WriteLine(item);
            }

            // Assert
            Assert.Equal(100, fizzBuzzResponse.Count());
        }
    }
}
