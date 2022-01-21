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
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("temperatureInDegrees")]
        public double TemperatureC { get; set; }

        [JsonProperty("temperatureInFahrenheit")]
        public double TemperatureF => 32 + TemperatureC / 0.5556;

        [JsonProperty("summary")]
        public string? Summary { get; set; }
    }
}
