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
        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("forecast")]
        public CityForecast Forecast { get; set; }
    }
}
