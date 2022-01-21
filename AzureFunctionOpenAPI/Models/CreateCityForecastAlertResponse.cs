using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.Models
{
    public class CreateCityForecastAlertResponse
    {
        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("alertId")]
        public string AlertId { get; set; }
    }
}
