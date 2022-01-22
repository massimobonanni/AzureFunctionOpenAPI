using AzureFunctionOpenAPI.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.OpenApi.Examples
{
    public class GetCityForecastsResponseExample :
        OpenApiExample<GetCityForecastsResponse>
    {
        public override IOpenApiExample<GetCityForecastsResponse> Build(NamingStrategy namingStrategy = null)
        {
            var example = OpenApiExampleResolver.Resolve("first",
                new GetCityForecastsResponse() {
                    NameFilter = "rome",
                    Cities= new List<CityForecast>() { 
                        new CityForecast() {
                            Name="Rome",
                            Forecasts= new List<WeatherForecast>() { 
                                new WeatherForecast() { 
                                    Date =new DateTime(2022,01,01),
                                    TemperatureC=10,
                                    Summary="Mild"
                                },
                                new WeatherForecast() {
                                    Date =new DateTime(2022,01,02),
                                    TemperatureC=12,
                                    Summary="Mild"
                                },
                                new WeatherForecast() {
                                    Date =new DateTime(2022,01,03),
                                    TemperatureC=8,
                                    Summary="Mild"
                                },
                                new WeatherForecast() {
                                    Date =new DateTime(2022,01,04),
                                    TemperatureC=11,
                                    Summary="Mild"
                                },
                                new WeatherForecast() {
                                    Date =new DateTime(2022,01,05),
                                    TemperatureC=9,
                                    Summary="Mild"
                                },
                            }
                        }
                    }
                },
                namingStrategy);

            this.Examples.Add(example);

            return this;
        }
    }
}
