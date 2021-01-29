using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WeatherApp.Views;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();

            var weatherPageViewModel = new WeatherPageViewModel();

            Detail = new NavigationPage(new WeatherPageView(weatherPageViewModel));
            Master = new SidePageView(new SidePageViewModel(weatherPageViewModel) { MasterDetailPage = this });
        }
    }
}
