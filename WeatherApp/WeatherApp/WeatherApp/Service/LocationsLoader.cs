using Newtonsoft.Json;
using WeatherApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace WeatherApp.Service
{
    public class LocationsLoader
    {
        private static LocationsLoader _instance;

        private LocationsLoader() 
        {
            AllLocations = new List<CitiesInfo>();
            LocationsNames = new List<string>();
            LocationsCountries = new List<string>();
        }
       
        public static LocationsLoader Instance
        {
            get => _instance == null ? _instance = new LocationsLoader() : _instance;
        }

        public List<CitiesInfo> AllLocations;
        public List<string> LocationsNames;
        public List<string> LocationsCountries;

        public void LoadLocations()
        {
            AllLocations = GetLocations();

            foreach (var element in AllLocations)
            {
                LocationsNames.Add(element.name);
                LocationsCountries.Add(element.country);
            }
        }

        private List<CitiesInfo> GetLocations()
        {
            var stream = Application.Current.GetType().Assembly.GetManifestResourceStream("WeatherApp.Resources.locations.json");

            using (var sr = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<List<CitiesInfo>>(sr.ReadToEnd());
            }
        }
    }
}
