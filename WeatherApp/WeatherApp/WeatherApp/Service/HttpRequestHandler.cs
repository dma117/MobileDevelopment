﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.Service
{
    public static class HttpRequestHandler
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        public static async Task<WeatherInfo> GetModelAsync(string cityName)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=f8a6eb17aa6438716b45950fa665d65f&units=metric");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<WeatherInfo>(responseBody);
            }
            catch (HttpRequestException e)
            {
                throw new Exception();
            }
        }

        public static async Task<ForecastInfo> GetForecastAsync(string cityName)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&appid=f8a6eb17aa6438716b45950fa665d65f&units=metric");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ForecastInfo>(responseBody);
            }
            catch (HttpRequestException e)
            {
                throw new Exception();
            }
        }

        public static bool InternetConnected()
        {
            return Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;
        }
    }
}
