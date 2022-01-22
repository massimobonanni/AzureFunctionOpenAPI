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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace AzureFunctionOpenAPI
{
    public class WeatherForecastsFunctions
    {
        [OpenApiOperation("getcityforecasts",
            new[] { "WeatherForecast" },
            Summary = "Returns the weather forecasts of cities",
            Description = "Returns the weather forecast for the next 5 days for cities that have the filter parameter in their name. If the filter is not set, it returns the weather forecast for all cities.",
            Visibility = OpenApiVisibilityType.Important)]

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


        [OpenApiOperation("getcityforecast",
            new[] { "WeatherForecast" },
            Summary = "Returns the weather forecasts for a specific city",
            Description = "Returns the weather forecast for the next 10 days for a specific city identified by its name.",
            Visibility = OpenApiVisibilityType.Important)]

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


        [OpenApiOperation("createcityforecastalert",
            new[] { "WeatherAlert" },
            Summary = "Create a new alert for a specific city",
            Description = "Create a weather forecast alert for a specific city. The alert has a specific duration and a temperature throshold. The alert will be fire when the city temperature will greater than the threshold.",
            Visibility = OpenApiVisibilityType.Important)]

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
