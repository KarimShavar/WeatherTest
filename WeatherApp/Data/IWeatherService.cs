﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Data
{
    public interface IWeatherService
    {
        Task<IList<WeatherReport>> GetWeatherForAllCitiesAsync();
        IDictionary<string, double> GetCityTemperatures();
    }
}
