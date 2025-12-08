using FizzBuzz.Rule.Engine.Rules;

namespace FizzBuzz.Rule.Engine
{
    public class FizzBuzzRuleEngine
    {
        private readonly List<IFizzBuzzRule> _rules;

        public FizzBuzzRuleEngine(IEnumerable<IFizzBuzzRule> rules)
        {
            _rules = rules.ToList();
        }

        public string Process(int number)
        {
            var rule = _rules.First(r => r.AppliesTo(number));
            return rule.GetOutput(number);
        }

        public IEnumerable<string> Run(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return Process(i);
            }
        }
    }
}