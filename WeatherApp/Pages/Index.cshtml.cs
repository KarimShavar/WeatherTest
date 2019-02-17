using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJSCore.Models;
using ChartJSCore.Models.Bar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Pages
{
    public class IndexModel : PageModel
    {
        public double[] MedianGlobalTemperatures { get; set; }
        public IDictionary<string, double> CityTemperatures { get; set; }

        public IList<WeatherReport> Weathers { get; set; }
        private readonly IWeatherService _weatherService;

        public IndexModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            MedianGlobalTemperatures = new[]{-33.0, -25.0, -22.0, -20.0, -10.0,
                                             5.0, 10.0, 12.0, 13.0, 23.0, 26.0, 30.0};
        }

        public async Task OnGetAsync()
        {
            Weathers = await _weatherService.GetWeatherForAllCitiesAsync();
            CityTemperatures = _weatherService.GetCityTemperatures();
        }

        public Chart TempChart()
        {
            var chart = new Chart();

            chart.Type = "bar";

            var data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>(CityTemperatures.Keys);

            var dataset = new BarDataset()
            {
                Label = "Temperatures",
                Data = CityTemperatures.Values.ToList(),
                BackgroundColor = new List<string>() { "46, 46, 46, 1" },
                BorderColor = new List<string>() { "46, 46, 46, 1" },
                BorderWidth = new List<int>() { 1 }
            };

            data.Datasets = new List<Dataset> {dataset};
            chart.Data = data;

            var options = new BarOptions()
            {
                Scales = new Scales(),
                BarPercentage = 0.7
            };

            var scales = new Scales()
            {
                YAxes = new List<Scale>()
                {
                    new CartesianScale()
                    {
                        Ticks = new CartesianLinearTick()
                        {
                            BeginAtZero = true
                        }
                    }
                }
            };

            options.Scales = scales;

            chart.Options = options;

            chart.Options.Layout = new Layout()
            {
                Padding = new Padding()
                {
                    PaddingObject = new PaddingObject()
                    {
                        Left = 10,
                        Right = 12
                    }
                }
            };

            return chart;
        }
    }
}
