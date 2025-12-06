using FizzBuzz.Engine;

namespace FizzBuzz.Tests
{
    public class FizzBuzzEngine_Fixture
    {
        public FizzBuzzEngine Engine { get; }

        public FizzBuzzEngine_Fixture()
        {
            Engine = new FizzBuzzEngine();
        }
    }
}