using Newtonsoft.Json;
using WeatherApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace WeatherApp.Service
{
    public static class Loader
    {
        public static List<CitiesInfo> GetLocations()
        {
            var stream = Application.Current.GetType().Assembly.GetManifestResourceStream("WeatherApp.Resources.locations.json");

            using (var sr = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<List<CitiesInfo>>(sr.ReadToEnd());
            }
        }
    }
}
