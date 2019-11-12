using Moq;
using MyOrg.Models;
using MyOrg.WeatherDataProcess;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyOrg.WeatherService.Tests
{
    public class ProcessInputFileTest
    {
        
        [Fact]
        public void Test1()
        {
            #region Arrange
            var mockedFile = new Mock<IFileToBeProcessed>(); 
            Dictionary<string, string> testCities = new Dictionary<string, string>();
            testCities.Add("Mumbai", "1275339");
            testCities.Add("Delhi", "1273294");
            IFileToBeProcessed fileToBe = new FileToBeProcessed("D:\\SourceFiles\\ExampleData - Copy.txt", "D:\\newOutput", testCities);
            
            var process = new ProcessInputFile(fileToBe);
            #endregion
            #region Act   
            var result = process.ProcessWeatherInfo();
            #endregion
            #region Assert      
            Assert.Equal(2, result);
            #endregion
        }
    }
}
