using AzureFunctionOpenAPI.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.OpenApi.Examples
{
    public class CreateCityForecastAlertRequestExample :
        OpenApiExample<CreateCityForecastAlertRequest>
    {
        public override IOpenApiExample<CreateCityForecastAlertRequest> Build(NamingStrategy namingStrategy = null)
        {
            var example = OpenApiExampleResolver.Resolve("first",
                new CreateCityForecastAlertRequest()
                {
                    DurationInDays = 10,
                    TemperatureThreshold = 25.5
                }, 
                namingStrategy);

            this.Examples.Add(example);

            return this;
        }
    }
}
