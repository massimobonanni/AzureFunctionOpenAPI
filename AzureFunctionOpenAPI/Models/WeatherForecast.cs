using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.Models
{
    public class WeatherForecast
    {
        [OpenApiProperty(Description = "Forecast date")]
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [OpenApiProperty(Description = "Expected temperature (in degrees centigrade)")]
        [JsonProperty("temperatureInDegrees")]
        public double TemperatureC { get; set; }

        [OpenApiProperty(Description = "Expected temperature (in fahrenheit)")]
        [JsonProperty("temperatureInFahrenheit")]
        public double TemperatureF => 32 + TemperatureC / 0.5556;

        [OpenApiProperty(Description ="Weather forecast")]
        [JsonProperty("summary")]
        public string? Summary { get; set; }
    }
}
