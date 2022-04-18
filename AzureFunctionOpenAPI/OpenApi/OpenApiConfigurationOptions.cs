using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.OpenApi
{
    public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
    {
         public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = "1.0.0",
            Title = "Awesome Weather REST API",
            Description = "Awesome Weather provides the best API to get updated and accurate weather forecasts.",
            TermsOfService = new Uri("https://github.com/massimobonanni/AzureFunctionOpenAPI"),
            Contact = new OpenApiContact()
            {
                Name = "AwesomeWeather",
                Email = "info@awesomeweather.com",
                Url = new Uri("https://github.com/massimobonanni/AzureFunctionOpenAPI"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        };
    }
}
