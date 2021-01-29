using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherApp.ViewModels;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPageView : ContentPage
    {
        public WeatherPageView(WeatherInfoViewModel weatherInfoViewModel)
        {
            InitializeComponent();
            BindingContext = weatherInfoViewModel;
        }
    }
}
