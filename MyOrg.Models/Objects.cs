using System;
using System.Collections.Generic;

namespace MyOrg.Models
{
    public class City
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }        
    }
    public interface IFileToBeProcessed
    {
        string FileSource { get; set; }
        string FileOutputDestination { get; set; }
        List<City> CitiesInFile { get; set; }
    }
    public class FileToBeProcessed : IFileToBeProcessed
    {
        public string FileSource { get ; set ; }
        public string FileOutputDestination { get; set; }
        public List<City> CitiesInFile { get; set; }
        public FileToBeProcessed(string source,string output,Dictionary<string,string> allCities)
        {
            this.FileSource = source;
            this.FileOutputDestination = output;
            this.CitiesInFile = getCities(allCities);
        }

        private List<City> getCities(Dictionary<string, string> allCities)
        {
            List<City> cities = new List<City>();
            foreach(KeyValuePair<string,string> city in allCities)
            {
                cities.Add(new City { Name = city.Key, ZipCode = city.Value });
            }
            return cities;
        }
    }
}
