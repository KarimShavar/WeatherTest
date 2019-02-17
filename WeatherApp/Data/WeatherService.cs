using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using WeatherApp.Models;


namespace WeatherApp.Data
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherCall _call;
        private IList<OpenWeatherResponseDto> _weathers;

        private readonly IList<WeatherReport> _weatherReports;
        private readonly IDictionary<string, double> _cityTemperatures;

        public WeatherService(WeatherCall call)
        {
            _call = call;
            _weatherReports = new List<WeatherReport>();
            _cityTemperatures = new Dictionary<string, double>();

        }
        public async Task<IList<WeatherReport>> GetWeatherForAllCitiesAsync()
        {
            var apiCall = await _call.GetAll();
            _weathers = _call.Weathers.list;

            if (!_weathers.Any())
            {
                throw new Exception("Error retrieving data from API. List is empty.");
            }

            ConvertDtoIntoWeatherReport(_weathers);

            return _weatherReports;
        }

        private void ConvertDtoIntoWeatherReport(IList<OpenWeatherResponseDto> weatherResponseDto)
        {
            foreach (var openWeatherResponseDto in _weathers)
            {
                _weatherReports.Add(new WeatherReport()
                {
                    Id = openWeatherResponseDto.id,
                    City = openWeatherResponseDto.name,
                    Temperature = openWeatherResponseDto.main.temp,
                    Humidity = openWeatherResponseDto.main.humidity,
                    Pressure = openWeatherResponseDto.main.pressure,
                    Description = openWeatherResponseDto.weather[0].description
                });
            }
        }

        public IDictionary<string, double> GetCityTemperatures()
        {
            if (_weathers.Any())
            {
                foreach (var response in _weathers)
                {
                    _cityTemperatures.TryAdd(response.name, response.main.temp);
                }
            }
            return _cityTemperatures;
        }
    }
}