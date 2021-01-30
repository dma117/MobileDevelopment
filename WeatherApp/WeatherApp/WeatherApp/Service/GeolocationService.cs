using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace WeatherApp.Service
{
    public class GeolocationService
    {
        private static GeolocationService _instance;
        private readonly string _defaultLocation = "Moscow";

        private GeolocationService() { }

        public static GeolocationService Instance
        {
            get => _instance == null ? _instance = new GeolocationService() : _instance;
        }

        public async Task<string> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    var weatherResponse = await HttpRequestHandler.GetModelAsync(location.Longitude, location.Latitude);

                    return weatherResponse.name;
                }

            }
            catch (Exception ex)
            {
            }

            return _defaultLocation;
        }
    }
}
