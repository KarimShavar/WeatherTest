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
        private IList<OpenWeatherResponse> _weathers;
        private IDictionary<string, double> _cityTemperatures;

        public WeatherService(WeatherCall call)
        {
            _call = call;
        }

        public async Task<IList<OpenWeatherResponse>> GetWeatherForAllCitiesAsync()
        {
            var weathers = await _call.GetAll();
            _weathers = _call.Weathers.list;
            return _weathers;
        }

        public IDictionary<string, double> GetCityTemperatures()
        {
            _cityTemperatures = new Dictionary<string, double>();
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