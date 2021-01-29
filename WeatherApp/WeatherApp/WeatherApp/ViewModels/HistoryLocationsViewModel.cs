using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using WeatherApp.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace WeatherApp.ViewModels
{
    public class HistoryLocationsViewModel : BaseViewModel
    {
        private string _currentLocation;

        public HistoryLocationsViewModel()
        {
            LocationsHistory = new ObservableCollection<string>();
            _currentLocation = String.Empty;

            AddLocationCommand = new Command(OpenLocations);
            ChooseLocationCommand = new Command(ChooseLocation);
        }

        public ObservableCollection<String> LocationsHistory { get; set; }

        public ICommand AddLocationCommand { get; private set; }
        public ICommand ChooseLocationCommand { get; private set; }

        public INavigation Navigation { get; set; }

        public string ChosenLocation
        {
            get => _currentLocation;

            set
            {
                _currentLocation = value;
                SaveChosenLocation();
            }
        }

        private void OpenLocations()
        {
            Navigation.PushAsync(new LocationsView(new LocationsViewModel(this)));
        }

        private void ChooseLocation(object obj)
        {
            var location = obj as string;

            if (location == null)
                return;

            ChosenLocation = location;
            SaveChosenLocation();

            Application.Current.MainPage.DisplayAlert("Location", "You've successfully chosen " + location, "Ok");
        }

        private void SaveChosenLocation()
        {
            if (LocationsHistory.All(x => x != _currentLocation))
            {
                LocationsHistory.Add(_currentLocation);
            }
        }
    }
}
