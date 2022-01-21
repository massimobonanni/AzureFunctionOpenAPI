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
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("forecasts")]
        public List<WeatherForecast> Forecasts { get; set; }
    }
}
