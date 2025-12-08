using FizzBuzz.Engine;
using FizzBuzz.Engine.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace FizzBuzz.Tests.ApiTests
{
    public class FizzBuzzApi_Should : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FizzBuzzApi_Should(WebApplicationFactory<Program> application)
        {
            _client = application.CreateClient();
        }

        [Fact]
        public async Task ReturnBadRequestWhen_CallingForRangeAnd_EndGreaterThanStart()
        {
            // Arrange
            int start = 10;
            int end = 5;

            var fizzBuzzResponse = await _client.GetAsync($"/api/FizzBuzz/ForRange?Start={start}&End={end}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, fizzBuzzResponse.StatusCode);
        }

        [Fact]
        public async Task ReturnBadRequestWhen_CallingForRangeReverseAnd_StartGreaterThanEnd()
        {
            // Arrange
            int start = 5;
            int end = 10;

            // Act
            var fizzBuzzResponse = await _client.GetAsync($"/api/FizzBuzz/ForRangeReverse?Start={start}&End={end}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, fizzBuzzResponse.StatusCode);
        }

        [Fact]
        public async Task ReturnOk_WhenSingleIntPassed()
        {
            // Act
            var response = await _client.GetAsync("/api/fizzbuzz/15");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReturnValues_ForRange1To5()
        {
            // Act
            var response = await _client.GetAsync("/api/FizzBuzz/ForRange?Start=1&End=5");

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            FizzBuzzResponse fizzBuzzResponse = JsonSerializer.Deserialize<FizzBuzzResponse>(json, options);

            // Assert
            Assert.Equal(new[] { "1", "2", Consts.Fizz, "4", Consts.Buzz }, fizzBuzzResponse.FizzBuzzResults);
        }

        [Fact]
        public async Task ReturnValues_ForRangeReverse10To5()
        {
            // Act
            var response = await _client.GetAsync("/api/FizzBuzz/ForRangeReverse?Start=10&End=5");

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            FizzBuzzResponse fizzBuzzResponse = JsonSerializer.Deserialize<FizzBuzzResponse>(json, options);

            // Assert
            Assert.Equal(new[] { Consts.Buzz, Consts.Fizz, "8", "7", Consts.Fizz, Consts.Buzz }, fizzBuzzResponse.FizzBuzzResults);
        }

        [Theory]
        [InlineData(3, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(7, "7")]
        public async Task ReturnValues_ForSingle(int input, string expected)
        {
            // Act
            var response = await _client.GetAsync($"/api/FizzBuzz/{input}");

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            FizzBuzzResponse fizzBuzzResponse = JsonSerializer.Deserialize<FizzBuzzResponse>(json, options);

            // Assert
            Assert.Equal(expected, fizzBuzzResponse.FizzBuzzResults[0]);
        }
    }
}