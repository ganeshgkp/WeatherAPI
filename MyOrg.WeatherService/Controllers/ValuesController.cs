using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyOrg.Common.Helper;
using MyOrg.Models;
using MyOrg.WeatherDataProcess;

namespace MyOrg.WeatherService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string inputFile = new ConfigurationHelper().GetConfig("AppSettings", "Directories", "Source") + "\\" + "ExampleData.txt";
            IFileToBeProcessed fileInfo = new FileToBeProcessed(inputFile,
                                          new ConfigurationHelper().GetConfig("AppSettings", "Directories", "Destination"),
                                          FileUtility.ReadCityAndZipCode(inputFile));
            ProcessInputFile file = new ProcessInputFile(fileInfo);
            int countOfCities = file.ProcessWeatherInfo();
            return new string[] { "CitiesProcessed", Convert.ToString(countOfCities) };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
