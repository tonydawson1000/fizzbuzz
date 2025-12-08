using System.ComponentModel.DataAnnotations;

namespace FizzBuzz.Api.Models
{
    public class FizzBuzzRangeRequest
    {
        [Required]
        public int Start { get; set; }

        [Required]
        public int End { get; set; }
    }
}