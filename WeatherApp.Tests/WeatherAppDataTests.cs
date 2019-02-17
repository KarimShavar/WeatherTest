using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Tests
{
    [TestClass]
    public class WeatherAppDataTests
    {
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
            Assert.IsTrue(300 == actual.id);
            Assert.IsTrue("London" == actual.name);
            Assert.IsTrue(Math.Abs(280.32D - actual.main.temp) < 0.01);
        }
    }
}
