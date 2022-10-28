using System;
namespace ContactsAppWeb.HTTPClientHelper
{
    public class WeatherForecastAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7268/WeatherForecast");
            return client;
        }
    }
    public class ContactsAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7268/api/contacts/");
            return client;
        }
    }
}

