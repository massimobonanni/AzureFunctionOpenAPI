using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
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
        [OpenApiProperty(Description = "Duration (in days) of the alert")]
        [JsonProperty("durationInDays")]
        public int? DurationInDays { get; set; }

        [OpenApiProperty(Description = "Temperature threshold beyond which the alert is raised")]
        [JsonProperty("temperatureThreshold")]
        public double? TemperatureThreshold { get; set; }

        public bool IsValid()
        {
            return DurationInDays.HasValue && TemperatureThreshold.HasValue;
        }
    }
}
