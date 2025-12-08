using Microsoft.AspNetCore.Mvc.Testing;

namespace FizzBuzz.Tests.ApiTests
{
    public class FizzBuzzApi_Fixture : IDisposable
    {
        public HttpClient Client { get; }

        private readonly WebApplicationFactory<Program> _factory;

        public FizzBuzzApi_Fixture()
        {
            // Create WebApplicationFactory for your Program.cs
            _factory = new WebApplicationFactory<Program>();

            // Create an HttpClient to call the in-memory test server
            Client = _factory.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}