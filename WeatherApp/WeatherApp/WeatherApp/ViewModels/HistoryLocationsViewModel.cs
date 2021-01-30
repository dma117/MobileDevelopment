using System;
using System.Windows.Input;
using Xamarin.Forms;
using WeatherApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherApp.Service;


namespace WeatherApp.ViewModels
{
    public class HistoryLocationsViewModel : BaseViewModel
    {
        private string _currentLocation;
        private ObservableCollection<string> _locationsHistory;
        private WeatherPageViewModel _weatherPageViewModel;

        public HistoryLocationsViewModel(WeatherPageViewModel weatherPageViewModel)
        {
            _currentLocation = String.Empty;
            _weatherPageViewModel = weatherPageViewModel;
            _locationsHistory = new ObservableCollection<string>();
            
            LoadHistoryLocations();
            
            AddLocationCommand = new Command(OpenLocations);
            ChooseLocationCommand = new Command(ChooseLocation);
        }

        public ICommand AddLocationCommand { get; private set; }
        public ICommand ChooseLocationCommand { get; private set; }

        public INavigation Navigation { get; set; }

        public ObservableCollection<String> LocationsHistory
        {
            get => _locationsHistory;

            set
            {
                _locationsHistory = value;
                OnPropertyChanged();
            }
        }

        public string ChosenLocation
        {
            get => _currentLocation;

            set
            {
                if (value != _currentLocation)
                {
                    if (_weatherPageViewModel.ChangeCurrentLocation(value))
                    {
                        _currentLocation = value;
                        Application.Current.MainPage.DisplayAlert("Location", "You've successfully chosen " + _currentLocation, "Ok");
                        SaveChosenLocation();
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Location", "Oops, " + _currentLocation + " is your current location", "Ok");
                }
            }
        }

        private void LoadHistoryLocations()
        {
            LocationsHistory = new ObservableCollection<string>(Saver.Instance.GetHistory());
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
        }

        private void SaveChosenLocation()
        {
            if (LocationsHistory.All(x => x != _currentLocation))
            {
                LocationsHistory.Add(_currentLocation);
                Saver.Instance.SerializeHistory(LocationsHistory);
            }
        }
    }
}
