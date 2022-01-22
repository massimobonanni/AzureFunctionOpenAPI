using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.Models
{
    public class CityForecast
    {

        [OpenApiProperty(Description ="Name of the city")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [OpenApiProperty(Description = "List of the weather forecasts")]
        [JsonProperty("forecasts")]
        public List<WeatherForecast> Forecasts { get; set; }
    }
}
