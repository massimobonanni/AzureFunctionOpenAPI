using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
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
        [OpenApiProperty(Description = "City name filter")]
        [JsonProperty("nameFilter")]
        public string NameFilter { get; set; }

        [OpenApiProperty(Description = "List of weather forecasts for cities that meet the search criteria")]
        [JsonProperty("cities")]
        public List<CityForecast> Cities { get; set; }
    }
}
