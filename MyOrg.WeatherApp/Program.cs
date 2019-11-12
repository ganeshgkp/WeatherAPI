using MyOrg.Common.Helper;
using MyOrg.Models;
using MyOrg.WeatherDataProcess;
using System;

namespace MyOrg.WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileToBeProcessed fileInfo = new FileToBeProcessed(new ConfigurationHelper().GetConfig("AppSettings", "Directories", "Source"),
                new ConfigurationHelper().GetConfig("AppSettings", "Directories", "Destination"),
                FileUtility.ReadCityAndZipCode(new ConfigurationHelper().GetConfig("AppSettings", "Directories", "Source"))
                );
            ProcessInputFile file = new ProcessInputFile(fileInfo);
            int countOfCities = file.ProcessWeatherInfo();
        }
    }
}
