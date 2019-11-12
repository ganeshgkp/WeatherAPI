using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyOrg.Integration.WeatherAPI
{
    public class OpenWeatherMap
    {
        private IAPIClient apiClient;
        private const string cityZipCode = "cityZipCode";
        private const string apiKey = "apiKey";
        private string internalURI = "/data/2.5/weather?id="+ cityZipCode + "&appid="+apiKey+"";
        
        public OpenWeatherMap(IAPIClient client)
        {
            this.apiClient = client;
        }

        public PresentWeather GetCurrentWeather(string zipCode)
        {
            var json = RunAsync(apiClient.APIKey, zipCode).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<PresentWeather>(json);
        }

        private async Task<string> RunAsync(string key, string zipCode)
        {
            var result = "";
            try
            {
                result = await GetWeatherAsync(zipCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        private async Task<string> GetWeatherAsync( string zipCode)
        {
            var result = string.Empty;           

            HttpResponseMessage response = await apiClient.GetHttpClient().GetAsync(internalURI.Replace(cityZipCode, zipCode).Replace(apiKey, apiClient.APIKey));
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {                
                Console.WriteLine(response.ToString());
            }
            return result;
        }
    }
}
