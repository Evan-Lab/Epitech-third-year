using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using Newtonsoft.Json.Linq;

public class Weather: IAction
{
    public Weather() {}
    public async Task <double> getWeather(string city) {
        string apiKey = "5356ecc0551270caf29c085fbc01e6f0";
        string units = "metric";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units={units}&appid={apiKey}";
        double temperature = 0;
        try {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(json);
                    JObject jsonObject = JObject.Parse(json);

                    temperature = (double)jsonObject["main"]["temp"];

                    Console.WriteLine($"Temperature: {temperature}°C");
                } else {
                    Console.WriteLine($"API request failed with status code: {response.StatusCode}");
                    return -1000;
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return temperature;
    }
}