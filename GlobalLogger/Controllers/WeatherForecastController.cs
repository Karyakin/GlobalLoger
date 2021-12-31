using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GlobalLogger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            Log.Information("Fetching all the Students from the storage");
            //throw new Exception("Exception while fetching all the students from the storage.");
            //throw new AccessViolationException("55555555555Violation Exception while accessing the resource.");
            int a = 1;
            int b = 0;
            //int c = a / b;

            if (a != 0)
            {
                //Log.Error("Собственный метод не равно нулю");
                throw new ArgumentException("значение не равно нулю");
            }

            return Ok("students");
        }
    }
}