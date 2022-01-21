using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctionOpenAPI.Models;
using AzureFunctionOpenAPI.Utilities;
using System.Linq;

namespace AzureFunctionOpenAPI
{
    public class WeatherForecastsFunctions
    {
        [FunctionName(nameof(GetCityForecasts))]
        public IActionResult GetCityForecasts(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "weatherforecasts")] HttpRequest req,
           ILogger log)
        {
            string cityName = req.Query["cityName"];

            var response = new GetCityForecastsResponse() { NameFilter = cityName };
            response.Cities = ForecastsUtility.GenerateCityForecasts(cityName, 5).ToList();

            return new OkObjectResult(response);
        }

        [FunctionName(nameof(GetCityForecast))]
        public IActionResult GetCityForecast(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "weatherforecasts/{cityName}")] HttpRequest req,
           string cityName,
           ILogger log)
        {
            var forecast = ForecastsUtility.GenerateCityForecast(cityName, 10);
            if (forecast == null)
                return new NotFoundObjectResult("City not found");

            var response = new GetCityForecastResponse()
            {
                CityName = cityName,
                Forecast = forecast
            };

            return new OkObjectResult(response);
        }

        [FunctionName(nameof(CreateCityForecastAlert))]
        public async Task<IActionResult> CreateCityForecastAlert(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "weatherforecasts/{cityName}/alert")] HttpRequest req,
           string cityName,
           ILogger log)
        {
            if (!ForecastsUtility.ExistsCity(cityName))
                return new NotFoundObjectResult("City not found");

            CreateCityForecastAlertRequest request = null;
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                request = JsonConvert.DeserializeObject<CreateCityForecastAlertRequest>(requestBody);
            }
            catch
            {
                return new BadRequestResult();
            }

            if (!request.IsValid())
                return new BadRequestResult();

            var response = new CreateCityForecastAlertResponse()
            {
                CityName = cityName,
                AlertId = Guid.NewGuid().ToString()
            };

            return new OkObjectResult(response);
        }


    }
}
