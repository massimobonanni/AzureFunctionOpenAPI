using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.Models
{
    public class GetCityForecastsResponse
    {
        [JsonProperty("nameFilter")]
        public string NameFilter { get; set; }

        [JsonProperty("cities")]
        public List<CityForecast> Cities { get; set; }
    }
}
