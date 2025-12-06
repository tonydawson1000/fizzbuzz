namespace FizzBuzz.Engine
{
    public class FizzBuzzEngine
    {
        public List<string> GenerateFizzBuzzForRange(int start, int end)
        {
            //Validate Input
            if (start > end)
            {
                throw new ArgumentException(Consts.ErrorStartGreaterThanEnd);
            }

            var result = new List<string>();

            for (int i = start; i <= end; i++)
            {
                result.Add(GenerateFizzBuzzForSingle(i));
            }

            return result;
        }

        public string GenerateFizzBuzzForSingle(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                return Consts.FizzBuzz;
            }
            else if (number % 3 == 0)
            {
                return Consts.Fizz;
            }
            else if (number % 5 == 0)
            {
                return Consts.Buzz;
            }
            else
            {
                return number.ToString();
            }
        }
    }
}