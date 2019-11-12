using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyOrg.Common.Helper
{
    public static class FileUtility
    {
        private const char fileDelimeter = '=';
        public static DirectoryInfo CreaeFolderStructure(string parentFolder)
        {            
            var year = DateTime.Now.ToString("yyyy");
            var mon = DateTime.Now.ToString("MMMM");
            var todaysDate = DateTime.Now.ToString("dd-MMM-yyyy");
            var folder = Path.Combine(parentFolder, Path.Combine(year,Path.Combine(mon,todaysDate)));
            return Directory.CreateDirectory(folder);
        }       
        
        public static void WriteToJsonFile<T>(string destinationFile, T valueTobeWritten, bool append = false) where T : new()
        {
            TextWriter textWriter = null;
            try
            {
                var serializedValues = JsonConvert.SerializeObject(valueTobeWritten);                
                textWriter = new StreamWriter(destinationFile, append);
                textWriter.Write(serializedValues);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (textWriter != null)
                    textWriter.Close();
            }
        }        
        
        public static Dictionary<string,string> ReadCityAndZipCode(string sourceFilePath)
        {
            Dictionary<string, string> cityAndZipCodes = new Dictionary<string, string>();
            if (!File.Exists(sourceFilePath)) return null;

            char[] delimiters = new char[] { fileDelimeter };
            var lines = File.ReadLines(sourceFilePath);
            foreach (string line in lines)
            {
                if(!string.IsNullOrEmpty(line))
                {
                    cityAndZipCodes.Add( line.Split(delimiters).ElementAt(1), line.Split(delimiters).ElementAt(0));
                }
            }
            return cityAndZipCodes;
        }
    }
}
