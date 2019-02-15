using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<OpenWeatherResponse> Weathers { get; set; }
        private readonly IWeatherService _weatherService;

        public IndexModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public void OnGetAsync()
        {
            this.Weathers = _weatherService.GetWeatherForAllCitiesAsync().Result;
        }
    }
}
