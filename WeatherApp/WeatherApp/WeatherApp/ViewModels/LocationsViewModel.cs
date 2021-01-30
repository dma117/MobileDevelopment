using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using WeatherApp.Service;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;


namespace WeatherApp.ViewModels
{
    public class LocationsViewModel : BaseViewModel
    {
        private List<Location> _locations;
        private List<Location> _currentLocations;
        private string _typedText;

        public LocationsViewModel(HistoryLocationsViewModel historyLocationsViewModel)
        {
            _locations = new List<Location>();
            _currentLocations = new List<Location>();
            _typedText = String.Empty;
            HistoryLocationsViewModel = historyLocationsViewModel;
            SetLocations();

            SearchLocations(_typedText);

            SearchCommand = new Command(SearchLocations);
        }
        public HistoryLocationsViewModel HistoryLocationsViewModel { get; set; }

        public ICommand SearchCommand { get; private set; }

        private void SetLocations()
        {
            _locations = LocationsLoader.Instance.AllLocations;
        }

        public List<Location> Locations
        {
            get => _currentLocations;

            set
            {
                _currentLocations = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _typedText;

            set
            {
                _typedText = value;
                SearchLocations(_typedText);
            }
        }

        private void SearchLocations(object obj)
        {
            var location = obj as string;

            if (location == null)
                return;

            Task.Run(() => {
                Locations = _locations.Where(x => x.Name.ToUpper().StartsWith(location.ToUpper())).ToList();
            });
        }
    }
}
