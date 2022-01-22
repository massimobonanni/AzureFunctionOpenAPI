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
    public class CreateCityForecastAlertResponseExample :
        OpenApiExample<CreateCityForecastAlertResponse>
    {
        public override IOpenApiExample<CreateCityForecastAlertResponse> Build(NamingStrategy namingStrategy = null)
        {
            var example = OpenApiExampleResolver.Resolve("first",
                new CreateCityForecastAlertResponse()
                {
                    CityName="Rome",
                    AlertId= "055905D5-7460-4362-A077-C3D8B031D607"
                }, 
                namingStrategy);

            this.Examples.Add(example);

            return this;
        }
    }
}
