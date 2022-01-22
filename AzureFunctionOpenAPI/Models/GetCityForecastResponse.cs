using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.Models
{
    public class GetCityForecastResponse
    {
        [OpenApiProperty(Description = "City name")]
        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [OpenApiProperty(Description = "Weather forecast for the city")]
        [JsonProperty("forecast")]
        public CityForecast Forecast { get; set; }
    }
}
