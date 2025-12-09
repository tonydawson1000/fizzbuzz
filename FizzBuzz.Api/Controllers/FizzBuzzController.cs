using FizzBuzz.Api.Models;
using FizzBuzz.Engine;
using FizzBuzz.Engine.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FizzBuzz.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class FizzBuzzController : ControllerBase
    {
        private readonly ILogger<FizzBuzzController> _logger;
        private readonly IFizzBuzzEngine _fizzBuzzEngine;

        private readonly IMemoryCache _cache;

        private readonly int _cacheDurationSeconds = 10;    //TODO : Move to config

        public FizzBuzzController(
            ILogger<FizzBuzzController> logger,
            IFizzBuzzEngine fizzBuzzEngine,
            IMemoryCache cache)
        {
            _logger = logger;
            _fizzBuzzEngine = fizzBuzzEngine;
            _cache = cache;
        }

        /// <summary>
        /// Evaluate a range of numbers.
        /// Example: /api/FizzBuzz/ForRange?Start=1&End=10
        /// </summary>
        [HttpGet("ForRange", Name = "FizzBuzzForRange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FizzBuzzResponse>> FizzBuzzForRange([FromQuery] FizzBuzzRangeRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string cacheKey = $"fizzbuzz_range_{request.Start}_{request.End}";

            if (_cache.TryGetValue(cacheKey, out FizzBuzzResponse cachedResponse))
            {
                cachedResponse.Message = "From Cache...";

                return Ok(cachedResponse);
            }

            var results = await _fizzBuzzEngine.GenerateFizzBuzzForRange(request.Start, request.End, cancellationToken);

            if(results.Success == false)
            {
                return BadRequest(results.Message);
            }

            // Store in cache with 10-second TTL
            _cache.Set(cacheKey, results, TimeSpan.FromSeconds(_cacheDurationSeconds));

            return Ok(results);
        }

        /// <summary>
        /// Evaluate a single number and return its FizzBuzz value.
        /// Example: /api/FizzBuzz/15
        /// </summary>
        [HttpGet("{value:int}", Name = "ForSingle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FizzBuzzResponse>> FizzBuzzForSingle(int value, CancellationToken cancellationToken)
        {
            string cacheKey = $"fizzbuzz_single_{value}";

            if (_cache.TryGetValue(cacheKey, out string cachedResult))
            {
                var fizzBuzzCachedResponse = new FizzBuzzResponse
                {
                    Success = true,
                    Message = "From Cache...",
                    FizzBuzzResults = new List<string> { cachedResult }
                };

                return Ok(fizzBuzzCachedResponse);
            }

            var result = await _fizzBuzzEngine.GenerateFizzBuzzForSingle(value, cancellationToken);

            // Store in cache with 10-second TTL
            _cache.Set(cacheKey, result.FizzBuzzResults[0], TimeSpan.FromSeconds(_cacheDurationSeconds));

            return Ok(result);
        }

        /// <summary>
        /// Evaluate a range of numbers - in reverse.
        /// Example: /api/FizzBuzz/ForRangeReverse?Start=30&End=10
        /// </summary>
        [HttpGet("ForRangeReverse", Name = "FizzBuzzForRangeReverse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FizzBuzzResponse>> FizzBuzzForRangeReverse([FromQuery] FizzBuzzRangeRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = await _fizzBuzzEngine.GenerateFizzBuzzForRangeReverse(request.Start, request.End, cancellationToken);

            if (results.Success == false)
            {
                return BadRequest(results.Message);
            }

            return Ok(results);
        }
    }
}