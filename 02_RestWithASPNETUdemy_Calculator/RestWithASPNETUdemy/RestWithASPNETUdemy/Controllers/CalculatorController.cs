using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    { 
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = convertToDecimal(firstNumber) + convertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input!");
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = convertToDecimal(firstNumber) - convertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input!");
        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = convertToDecimal(firstNumber) * convertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input!");
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = convertToDecimal(firstNumber) / convertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input!");
        }

        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            { 
                var sum = (convertToDecimal(firstNumber) + convertToDecimal(secondNumber)) / 2;
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input!");
        }

        [HttpGet("square-root/{firstNumber}")]
        public IActionResult SquareRoot(string firstNumber)
        {
            if (IsNumeric(firstNumber))
            {
                var sum = Math.Sqrt((double)convertToDecimal(firstNumber));
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input!");
        }

        private decimal convertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
                return decimalValue;

            return 0;
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            return double.TryParse(strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);

        }
    }
}