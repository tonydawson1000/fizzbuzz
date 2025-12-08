using FizzBuzz.Api.Models;
using FizzBuzz.Engine;
using FizzBuzz.Engine.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class FizzBuzzController : ControllerBase
    {
        private readonly ILogger<FizzBuzzController> _logger;
        private readonly IFizzBuzzEngine _fizzBuzzEngine;

        public FizzBuzzController(
            ILogger<FizzBuzzController> logger,
            IFizzBuzzEngine fizzBuzzEngine)
        {
            _logger = logger;
            _fizzBuzzEngine = fizzBuzzEngine;
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

            var results = await _fizzBuzzEngine.GenerateFizzBuzzForRange(request.Start, request.End, cancellationToken);

            if(results.Success == false)
            {
                return BadRequest(results.Message);
            }

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
            var result = await _fizzBuzzEngine.GenerateFizzBuzzForSingle(value, cancellationToken);
            
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