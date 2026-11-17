using api_web_new.DTO.Response;
using api_web_new.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_web_new.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/weatherforecast
    public class WeatherForecastController : ControllerBase
    {

        private readonly IProductService _service;

        public WeatherForecastController(IProductService service)
        {
            _service = service;
        }
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("allProducts/{id}")] // HTTPGET //params route 
        public async Task<ProductResponseDTO> GetAllProducts([FromServices] IProductService productService, int id)
        {
            return await productService.GetAll();
        }


        [HttpGet("allProducts")] // HTTPGET
        public async Task<ProductResponseDTO> GetAllProducts1()
        {
            
            return await _service.GetAll();
        }

        [HttpPost("allProducts")] // HTTPGET
        public List<string> GetAllProductsPost()
        {
            

            return new List<string>()
            {
               "POST OPERATION", "IPhone 16 pro", "Apple Watch Lite","DOCOMO SIM"
            };
        }

        [HttpDelete("allProducts")] // HTTPGET
        public List<string> GetAllProductsDelete()
        {
            return new List<string>()
            {
               "DELETE OPERATION", "IPhone 16 pro", "Apple Watch Lite","DOCOMO SIM"
            };
        }

        [HttpPut("allProducts")] // HTTPGET
        public List<string> GetAllProductsPut()
        {
            return new List<string>()
            {
               "PUT OPERATION", "IPhone 16 pro", "Apple Watch Lite","DOCOMO SIM"
            };
        }
    }
}
