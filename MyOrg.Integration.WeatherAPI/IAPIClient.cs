using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyOrg.Integration.WeatherAPI
{
    public interface IAPIClient
    {
        string APIUrl { get; set; }
        string APIKey { get; set; }
        HttpClient GetHttpClient();
    }
}
