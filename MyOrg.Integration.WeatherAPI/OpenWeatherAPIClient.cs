using MyOrg.Common.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MyOrg.Integration.WeatherAPI
{
    public class OpenWeatherAPIClient: IAPIClient
    {        
        public string APIUrl { get; set; } = new ConfigurationHelper().GetConfig("AppSettings", "WeatherAPI", "EndPoint");
        public string APIKey { get; set; } = new ConfigurationHelper().GetConfig("AppSettings", "WeatherAPI", "Key");

        private HttpClient Client;
        public OpenWeatherAPIClient()
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri(APIUrl);
            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public HttpClient GetHttpClient()
        {
            return this.Client;
        }
    }
}
