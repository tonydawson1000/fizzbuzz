namespace FizzBuzz.Rule.Engine.Rules
{
    public interface IFizzBuzzRule
    {
        bool AppliesTo(int number);
        string GetOutput(int number);
    }
}