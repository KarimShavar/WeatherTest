using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Moq;
using Newtonsoft.Json;
using WeatherApp.Data;
using WeatherApp.Models;
using WeatherApp.Pages;

namespace WeatherApp.Tests
{
    [TestClass]
    public class WeatherAppDataTests
    {
        [TestMethod]
        public void WeatherServiceReturnsWeatherReports()
        {
            //--Arrange
            var mock = new Mock<IWeatherService>();
            mock.Setup(o => o.GetWeatherForAllCitiesAsync())
                .ReturnsAsync(new List<WeatherReport>()
                {
                    new WeatherReport()
                    {
                        Id = 1,
                        City = "Worksop",
                        Description = "Windy",
                        Humidity = 84,
                        Pressure = 1008,
                        Temperature = 25
                    },
                    new WeatherReport()
                    {
                        Id = 2,
                        City = "Doncaster",
                        Description = "Fog",
                        Humidity = 89,
                        Pressure = 1010,
                        Temperature = 16
                    }
                    
                });
            mock.Setup(p => p.GetCityTemperatures())
                .Returns(new Dictionary<string, double>()
                {
                    {"Worksop", 25},
                    {"Doncaster", 16}
                });
            
            var main = new IndexModel(mock.Object);
            //-- Act
            var actual = main.OnGetAsync();
            
            //-- Assert
            Assert.IsTrue(actual.IsCompletedSuccessfully);
            
            Assert.AreEqual("Worksop", main.Weathers[0].City);
            Assert.AreEqual(1, main.Weathers[0].Id);
            Assert.AreEqual("Fog", main.Weathers[1].Description);
            
            Assert.IsTrue(main.Weathers.Any());
            Assert.IsTrue(main.CityTemperatures.Any());
        }
        
        [TestMethod]
        public void DataIsDeserializedIntoModelCorrectly()
        {
            //--Arrange
            var rawJsonString = "{\"coord\":{\"lon\":-0.13,\"lat\":51.51},\"weather\":" +
                                "[{\"id\":300,\"main\":\"Drizzle\",\"description\":\"light intensity drizzle\",\"icon\":\"09d\"}]," +
                                "\"base\":\"stations\"," +
                                "\"main\":{\"temp\":280.32,\"pressure\":1012,\"humidity\":81,\"temp_min\":279.15,\"temp_max\":281.15},\"visibility\":10000," +
                                "\"wind\":{\"speed\":4.1,\"deg\":80}," +
                                "\"clouds\":{\"all\":90},\"dt\":1485789600," +
                                "\"sys\":{\"type\":1,\"id\":5091,\"message\":0.0103," +
                                "\"country\":\"GB\",\"sunrise\":1485762037,\"sunset\":1485794875},\"id\":2643743,\"name\":\"London\",\"cod\":200}";
            //--Act
            var actual = JsonConvert.DeserializeObject<OpenWeatherResponseDto>(rawJsonString);

            //--Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(Math.Abs(-0.13D - actual.coord.lon) < 0.01);
            Assert.IsTrue(Math.Abs(2643743 - actual.id) < 0.01);
            Assert.IsTrue("London" == actual.name);
            Assert.IsTrue(Math.Abs(280.32D - actual.main.temp) < 0.01);
        }
    }
}
