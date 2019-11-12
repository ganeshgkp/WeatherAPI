using MyOrg.Common.Helper;
using MyOrg.Models;
using System.Linq;
using System.Collections.Generic;
using MyOrg.Integration.WeatherAPI;
using System.Threading.Tasks;
using System.IO;

namespace MyOrg.WeatherDataProcess
{
    public class ProcessInputFile
    {   
        IFileToBeProcessed FileBeingProcessed;
        public ProcessInputFile(IFileToBeProcessed fileInfo)
        {
            FileBeingProcessed = fileInfo;                        
        }        
        public int ProcessWeatherInfo()
        {            
            DirectoryInfo parentFolder;            
            if(FileBeingProcessed.CitiesInFile!= null & FileBeingProcessed.CitiesInFile.Count()>0)
            {
                OpenWeatherMap openWeatherAPI = new OpenWeatherMap(new OpenWeatherAPIClient());
                parentFolder = FileUtility.CreaeFolderStructure(FileBeingProcessed.FileOutputDestination);
                Parallel.ForEach(FileBeingProcessed.CitiesInFile, (city)=>
                {
                    PresentWeather currentWeatherOfCity = openWeatherAPI.GetCurrentWeather(city.ZipCode);
                    FileUtility.WriteToJsonFile<PresentWeather>($"{parentFolder.FullName}\\{city.Name}.txt", currentWeatherOfCity);
                });
            }
            else
            {
                return 0;
            }
            return FileBeingProcessed.CitiesInFile.Count();
        }
        
    }
}
