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
using AzureFunctionOpenAPI.OpenApi.Examples;

namespace AzureFunctionOpenAPI
{
    public class WeatherForecastsFunctions
    {
        [OpenApiOperation("getcityforecasts",
            new[] { "WeatherForecast" },
            Summary = "Returns the weather forecasts of cities",
            Description = "Returns the weather forecast for the next 5 days for cities that have the filter parameter in their name. If the filter is not set, it returns the weather forecast for all cities.",
            Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter("cityName",
            Summary = "Possible filter for the name of the city",
            Description = "If this parameter is used, the weather forecast for cities that have the parameter content in the name will be returned",
            In = Microsoft.OpenApi.Models.ParameterLocation.Query,
            Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(System.Net.HttpStatusCode.OK,
            "application/json",
            typeof(GetCityForecastsResponse),
            Summary = "List of weather forecasts for requested cities",
            Description = "List of cities resulting from the search and, for each city, the forecasts for the next 5 days",
            Example = typeof(GetCityForecastsResponseExample))]

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
        [OpenApiParameter("cityName",
            Summary = "Name of the city",
            Description = "Name of the city for which you want the weather forecast",
            In = Microsoft.OpenApi.Models.ParameterLocation.Path,
            Required = true,
            Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(System.Net.HttpStatusCode.OK,
            "application/json",
            typeof(GetCityForecastResponse),
            Summary = "The weather forecast for the requested city",
            Description = "The weather forecasts for the next 10 days related to the city you request",
            Example = typeof(GetCityForecastResponseExample))]
        [OpenApiResponseWithoutBody(System.Net.HttpStatusCode.NotFound,
            Summary = "City not found",
            Description = "Returns this message if you are searching for a city that doesn't exist")]

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
        [OpenApiParameter("cityName",
            Summary = "Name of the city",
            Description = "Name of the city for which you want to create an alert",
            In = Microsoft.OpenApi.Models.ParameterLocation.Path,
            Required = true,
            Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody("application/json",
            typeof(CreateCityForecastAlertRequest),
            Description = "Properties of the alert to be created",
            Required = true,
            Example = typeof(CreateCityForecastAlertRequestExample))]
        [OpenApiResponseWithBody(System.Net.HttpStatusCode.OK,
            "application/json",
            typeof(CreateCityForecastAlertResponse),
            Summary = "The alert id",
            Description = "The alert created. Contains the alertId and the name of the city",
            Example = typeof(CreateCityForecastAlertResponseExample))]
        [OpenApiResponseWithoutBody(System.Net.HttpStatusCode.NotFound,
            Summary = "City not found or request not valid",
            Description = "Returns this message if you try to create an alert for a city that doesn't exist or the request payload is not valid")]

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
