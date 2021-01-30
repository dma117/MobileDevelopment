using WeatherApp.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System;
using Xamarin.Forms;

namespace WeatherApp.Service
{
    public class Sharer
    {
        private static Sharer _instance;

        private Sharer()
        {

        }

        public static Sharer Instance
        {
            get => _instance == null ? _instance = new Sharer() : _instance;
        }

        public async Task ShareText(WeatherInfo weatherInfo)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = ConstructText(weatherInfo),
                Title = "Share Text"
            });
        }

        private string ConstructText(WeatherInfo weatherInfo)
        {
            string result = String.Empty;
            result += $"На данный момент температура в городе {weatherInfo.name} равна {weatherInfo.main.temp}\n";
            result += $"Влажность: {weatherInfo.main.humidity}\n";
            result += $"Скорость ветра: {weatherInfo.wind.speed}\n";
            result += $"Состояние погоды: {weatherInfo.weather[0].description}\n";

            ShowResult(result, weatherInfo.name);

            return result;
        }

        private async void ShowResult(string result, string name)
        {
            await Application.Current.MainPage.DisplayAlert($"Погода в городе {name}", result, "OK");
        }
    }
}
