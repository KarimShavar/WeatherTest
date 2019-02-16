using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp.Models;

namespace WeatherApp.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherCall : ControllerBase
    {
        public OpenWeatherResponseRectangle Weathers { get; private set; } 
        public OpenWeatherResponse SingleCityWeather { get; private set; }

        // GET: api/Weather
        // Get 15 cities info
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response =await client.GetAsync(
                        $"/data/2.5/box/city?bbox=16,51,22,54,10&appid={ApiStorage.ApiKey}&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    Weathers = JsonConvert.DeserializeObject<OpenWeatherResponseRectangle>(stringResult);
                    return Ok();
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest($"Error getting data:{ex.Message}");
                }
            }
        }

        // GET: api/Weather/5
        [HttpGet("{city}", Name = "Get")]
        public async Task<IActionResult> GetCityWeather(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={ApiStorage.ApiKey}&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    SingleCityWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                    return Ok(stringResult);
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest($"Error getting data:{ex.Message}");
                }
            }
        }
    }
}
