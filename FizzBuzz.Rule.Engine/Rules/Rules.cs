namespace FizzBuzz.Rule.Engine.Rules
{
    public class HolyMolyRule : IFizzBuzzRule
    {
        public bool AppliesTo(int number) => number % 8 == 0;
        public string GetOutput(int number) => "HolyMoly";
    }



    public class FizzRule : IFizzBuzzRule
    {
        public bool AppliesTo(int number) => number % 3 == 0;
        public string GetOutput(int number) => "Fizz";
    }

    public class BuzzRule : IFizzBuzzRule
    {
        public bool AppliesTo(int number) => number % 5 == 0;
        public string GetOutput(int number) => "Buzz";
    }

    public class FizzBuzzRule : IFizzBuzzRule
    {
        public bool AppliesTo(int number) => number % 15 == 0;
        public string GetOutput(int number) => "FizzBuzz";
    }

    // Default rule when no other rule applies
    public class NumberRule : IFizzBuzzRule
    {
        public bool AppliesTo(int number) => true;
        public string GetOutput(int number) => number.ToString();
    }
}