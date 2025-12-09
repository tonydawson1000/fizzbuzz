using FizzBuzz.Parallel.Engine.Responses;

namespace FizzBuzz.Parallel.Engine
{
    public class FizzBuzzParallelEngine
    {
        public async Task<string> EvaluateAsync(long value, CancellationToken token = default)
        {
            return await Task.FromResult(EvaluateInternal(value));
        }

        public async Task<FizzBuzzResponse> GetRangeAsync(long start, long end, CancellationToken token = default)
        {
            if (end < start)
                throw new ArgumentException("End must be >= Start");

            long count = (end - start) + 1;

            // Array is used for indexed writes → thread-safe & predictable ordering
            string[] results = new string[count];

            await Task.Run(() =>
            {
                System.Threading.Tasks.Parallel.For(start, end + 1, new ParallelOptions
                {
                    CancellationToken = token,
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                },
                i =>
                {
                    long index = i - start;
                    results[index] = EvaluateInternal(i);
                });

            }, token);

            return new FizzBuzzResponse
            {
                Start = start,
                End = end,
                FizzBuzzResults = results.ToList()
            };
        }

        private string EvaluateInternal(long value)
        {
            if (value % 15 == 0) return "FizzBuzz";
            if (value % 3 == 0) return "Fizz";
            if (value % 5 == 0) return "Buzz";
            return value.ToString();
        }
    }
}