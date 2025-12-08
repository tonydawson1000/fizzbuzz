using FizzBuzz.Rule.Engine;
using FizzBuzz.Rule.Engine.Rules;

namespace FizzBuzz.Tests.RuleEngineTests
{
    public class FizzBuzzRuleEngine_Fixture
    {
        public List<IFizzBuzzRule> Rules { get; set; }
        public FizzBuzzRuleEngine RuleEngine { get; }

        public FizzBuzzRuleEngine_Fixture()
        {
            Rules = new List<IFizzBuzzRule>
            {
                new FizzBuzzRule(),
                new HolyMolyRule(),
                new FizzRule(),
                new BuzzRule(),
                new NumberRule()
            };

            RuleEngine = new FizzBuzzRuleEngine(Rules);
        }
    }
}