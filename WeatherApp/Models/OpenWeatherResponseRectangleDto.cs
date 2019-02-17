using System.Collections.Generic;

namespace WeatherApp.Models
{
    public class OpenWeatherResponseRectangleDto
    {
        public string cod { get; set; }
        public double calctime { get; set; }
        public double cnt { get; set; }
        public List<OpenWeatherResponseDto> list { get; set; }
    }
}