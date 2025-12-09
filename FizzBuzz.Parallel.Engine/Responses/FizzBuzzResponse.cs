namespace FizzBuzz.Parallel.Engine.Responses
{
    public class FizzBuzzResponse
    {
        public long Start { get; set; }
        public long End { get; set; }

        public List<string> FizzBuzzResults { get; set; }
    }
}