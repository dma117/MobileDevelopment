using System;
using System.Windows.Input;
using Xamarin.Forms;
using WeatherApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherApp.Service;
using WeatherApp.Models;


namespace WeatherApp.ViewModels
{
    public class HistoryLocationsViewModel : BaseViewModel
    {
        private Location _currentLocation;
        private ObservableCollection<Location> _locationsHistory;
        private WeatherPageViewModel _weatherPageViewModel;

        public HistoryLocationsViewModel(WeatherPageViewModel weatherPageViewModel)
        {
            _currentLocation = new Location();
            _weatherPageViewModel = weatherPageViewModel;
            _locationsHistory = new ObservableCollection<Location>();

            LoadHistoryLocations();
            
            AddLocationCommand = new Command(OpenLocations);
            ChooseLocationCommand = new Command(ChooseLocation);
        }

        public ICommand AddLocationCommand { get; private set; }
        public ICommand ChooseLocationCommand { get; private set; }

        public INavigation Navigation { get; set; }

        public ObservableCollection<Location> LocationsHistory
        {
            get => _locationsHistory;

            set
            {
                _locationsHistory = value;
                OnPropertyChanged();
            }
        }

        public Location ChosenLocation
        {
            get => _currentLocation;

            set
            {
                if (value != _currentLocation)
                {
                    if (_weatherPageViewModel.ChangeCurrentLocation(value.Name))
                    {
                        _currentLocation = value;
                        Application.Current.MainPage.DisplayAlert("Location", "You've successfully chosen " + _currentLocation.Name, "Ok");
                        SaveChosenLocation();
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Location", "Oops, " + _currentLocation.Name + " is your current location", "Ok");
                }
            }
        }

        private void LoadHistoryLocations()
        {
            LocationsHistory = new ObservableCollection<Location>(Saver.Instance.GetHistory());
        }

        private void OpenLocations()
        {
            Navigation.PushAsync(new LocationsView(new LocationsViewModel(this)));
        }

        private void ChooseLocation(object obj)
        {
            var location = obj as Location;

            if (location == null)
                return;

            ChosenLocation = location;
        }

        private void SaveChosenLocation()
        {
            if (LocationsHistory.All(x => x != _currentLocation))
            {
                LocationsHistory.Add(_currentLocation);
                Saver.Instance.SerializeHistory(LocationsHistory);
            }
        }

        public void CheckCurrentLocation()
        {
            _currentLocation = new Location()
            {
                Name = _weatherPageViewModel.LocationName,
                Country = _weatherPageViewModel.Country
            };
        }
    }
}
