using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WeatherApp.Helpers
{
    public class ErrorMessenger
    {
        private static ErrorMessenger _instance;

        private ErrorMessenger() { }

        public static ErrorMessenger Instance
        {
            get => _instance == null ? _instance = new ErrorMessenger() : _instance;
        }

        public void ChangeLocationFailed()
        {
            Application.Current.MainPage.DisplayAlert("Error", "No internet connection. Your location cannot be changed.", "Ok");
        } 

        public void LoadWeatherFailed()
        {
            Application.Current.MainPage.DisplayAlert("Error", "No internet connection. The weather is not up to date.", "Ok");
        }
    }
}
