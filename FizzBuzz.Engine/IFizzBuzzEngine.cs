using FizzBuzz.Engine.Responses;

namespace FizzBuzz.Engine
{
    public interface IFizzBuzzEngine
    {
        public Task<FizzBuzzResponse> GenerateFizzBuzzForRange(int start, int end, CancellationToken cancellationToken);

        public Task<FizzBuzzResponse> GenerateFizzBuzzForRangeReverse(int start, int end, CancellationToken cancellationToken);

        public Task<FizzBuzzResponse> GenerateFizzBuzzForSingle(int number, CancellationToken cancellationToken);
    }
}