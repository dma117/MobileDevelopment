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
            LocationDescriptions = new List<CitiesInfo>();
            //LocationsNames = new List<string>();
            //LocationsCountries = new List<string>();
            AllLocations = new List<Location>();
        }
       
        public static LocationsLoader Instance
        {
            get => _instance == null ? _instance = new LocationsLoader() : _instance;
        }

        public List<CitiesInfo> LocationDescriptions;
        //public List<string> LocationsNames;
        //public List<string> LocationsCountries;

        public List<Location> AllLocations;

        public void LoadLocations()
        {
            LocationDescriptions = GetLocations();

            foreach (var element in LocationDescriptions)
            {
                AllLocations.Add(new Location() 
                { 
                    Name = element.name, 
                    Country = element.country 
                });
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
