namespace FizzBuzz.Engine.Responses
{
    public class FizzBuzzResponse : BaseResponse
    {
        public FizzBuzzResponse() : base() { }

        public List<string> FizzBuzzResults { get; set; } = new List<string>();
    }
}