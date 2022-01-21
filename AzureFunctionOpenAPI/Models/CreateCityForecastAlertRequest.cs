using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.Models
{
    public class CreateCityForecastAlertRequest
    {
        [JsonProperty("durationInDays")]
        public int? DurationInDays { get; set; }

        [JsonProperty("temperatureThreshold")]
        public double? TemperatureThreshold { get; set; }

        public bool IsValid()
        {
            return DurationInDays.HasValue && TemperatureThreshold.HasValue;
        }
    }
}
