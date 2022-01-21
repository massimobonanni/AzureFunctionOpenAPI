using AzureFunctionOpenAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionOpenAPI.Utilities
{
    internal class ForecastsUtility
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string[] Cities = new[]
        {
            "London", "New York", "Rome", "Milan", "Madrid", "Paris", "Berlin", "Munich", "Stocholm", "Chicago","Los Angeles","Seattle"
        };

        public static bool ExistsCity(string cityName)
        {
            return Cities.Any(c => c.Equals(cityName, StringComparison.InvariantCultureIgnoreCase));
        }

        public static IEnumerable<WeatherForecast> GenerateWeatherForecasts(int numberOfForecasts)
        {
            return Enumerable.Range(1, numberOfForecasts).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
        }

        public static IEnumerable<CityForecast> GenerateCityForecasts(string nameFilter, int numberOfForecasts)
        {
            var cities = Cities.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                nameFilter = nameFilter.ToLower();
                cities = cities.Where(c => c.ToLower().Contains(nameFilter));
            }

            return cities.Select(c => new CityForecast()
            {
                Name = c,
                Forecasts = ForecastsUtility.GenerateWeatherForecasts(numberOfForecasts).ToList()
            });
        }

        public static CityForecast GenerateCityForecast(string cityName, int numberOfForecasts)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return null;

            var city = Cities.FirstOrDefault(c => c.Equals(cityName, StringComparison.InvariantCultureIgnoreCase));

            if (city == null)
                return null;

            return new CityForecast()
            {
                Name = city,
                Forecasts = ForecastsUtility.GenerateWeatherForecasts(numberOfForecasts).ToList()
            };
        }
    }
}
