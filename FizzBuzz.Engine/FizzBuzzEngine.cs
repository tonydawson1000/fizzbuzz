using FizzBuzz.Engine.Responses;

namespace FizzBuzz.Engine
{
    public class FizzBuzzEngine : IFizzBuzzEngine
    {
        public async Task<FizzBuzzResponse> GenerateFizzBuzzForRange(int start, int end, CancellationToken cancellationToken)
        {
            var result = new FizzBuzzResponse();

            //Validate Input
            if (start > end)
            {
                result.Success = false;

                result.Message = Consts.ErrorStartGreaterThanEnd;

                return result;
            }

            for (int i = start; i <= end; i++)
            {
                var response = await GenerateFizzBuzzForSingle(i, cancellationToken);

                result.FizzBuzzResults.Add(response.FizzBuzzResults[0]);
            }

            return result;
        }

        public async Task<FizzBuzzResponse> GenerateFizzBuzzForRangeReverse(int start, int end, CancellationToken cancellationToken)
        {
            var result = new FizzBuzzResponse();

            //Validate Input
            if (end > start)
            {
                result.Success = false;

                result.Message = Consts.ErrorEndGreaterThanStart;

                return result;
            }

            for (int i = start; i >= end; i--)
            {
                var response = await GenerateFizzBuzzForSingle(i, cancellationToken);

                result.FizzBuzzResults.Add(response.FizzBuzzResults[0]);
            }

            return result;
        }

        public async Task<FizzBuzzResponse> GenerateFizzBuzzForSingle(int number, CancellationToken cancellationToken)
        {
            var result = new FizzBuzzResponse();

            if (number % 3 == 0 && number % 5 == 0)
            {
                await LogAsync("FizzBuzz hit");

                result.FizzBuzzResults.Add(Consts.FizzBuzz);

                return result;
            }
            else if (number % 3 == 0)
            {
                await LogAsync("Fizz hit");

                result.FizzBuzzResults.Add(Consts.Fizz);
                
                return result;
            }
            else if (number % 5 == 0)
            {
                await LogAsync("Buzz hit");

                result.FizzBuzzResults.Add(Consts.Buzz);

                return result;
            }
            else
            {
                result.FizzBuzzResults.Add(number.ToString());

                return result;
            }
        }

        // Example of async helper (could be real I/O)
        private async Task LogAsync(string message)
        {
            await Task.Delay(5); // simulate async log
        }
    }
}