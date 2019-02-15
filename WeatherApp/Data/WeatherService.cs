using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using WeatherApp.Models;


namespace WeatherApp.Data
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherCall _call;
        public IList<OpenWeatherResponse> Weathers;

        public WeatherService(WeatherCall call)
        {
            _call = call;
        }

        public async Task<IList<OpenWeatherResponse>> GetWeatherForAllCitiesAsync()
        {
            var weathers = await _call.GetAll();
            Weathers = _call.Weathers.list;
            return Weathers;
        }
    }
}