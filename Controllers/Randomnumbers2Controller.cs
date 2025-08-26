using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Randomnumbers2.Controllers

{
    using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace RandomNumbersAPI.Controllers
{
    [ApiController]
    [Route("random")]
    public class RandomController : ControllerBase
    {
        Random rnd = new Random();

        
        [HttpGet("number")]
        public IActionResult GetNumber(int min = 0, int max = int.MaxValue)
        {
            if (min > max)
            {
                return BadRequest("El valor de min no puede ser mayor que max.");
            }

            int valor = rnd.Next(min, max); // genera entre [min, max)
            return Ok(new { result = valor });
        }

        
        [HttpGet("decimal")]
        public IActionResult GetDecimal()
        {
            double valor = rnd.NextDouble(); // entre 0 y 1
            return Ok(new { result = valor });
        }

        [HttpGet("string")]
        public IActionResult GetString(int length = 8)
        {
            if (length < 1 || length > 1024)
            {
                return BadRequest("La longitud debe estar entre 1 y 1024.");
            }

            string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int pos = rnd.Next(0, letras.Length);
                sb.Append(letras[pos]);
            }

            return Ok(new { result = sb.ToString() });
        }
        
        [HttpPost("custom")]
        public IActionResult Custom([FromBody] CustomRequest body)
        {
            if (body.Type == "number")
            {
                if (body.Min > body.Max)
                {
                    return BadRequest("El min no puede ser mayor que max.");
                }
                int num = rnd.Next(body.Min, body.Max);
                return Ok(new { result = num });
            }
            else if (body.Type == "decimal")
            {
                int dec = body.Decimals > 0 ? body.Decimals : 2;
                double val = Math.Round(rnd.NextDouble(), dec);
                return Ok(new { result = val });
            }
            else if (body.Type == "string")
            {
                int len = body.Length > 0 ? body.Length : 8;
                if (len < 1 || len > 1024)
                {
                    return BadRequest("La longitud debe estar entre 1 y 1024.");
                }

                string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < len; i++)
                {
                    int pos = rnd.Next(0, letras.Length);
                    sb.Append(letras[pos]);
                }

                return Ok(new { result = sb.ToString() });
            }
            else
            {
                return BadRequest("El type debe ser 'number', 'decimal' o 'string'.");
            }
        }
    }

    public class CustomRequest
    {
        public string Type { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Decimals { get; set; }
        public int Length { get; set; }
    }
}

}