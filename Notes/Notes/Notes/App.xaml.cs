using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.View;
using Notes.ViewModel;

namespace Notes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListNotesView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
