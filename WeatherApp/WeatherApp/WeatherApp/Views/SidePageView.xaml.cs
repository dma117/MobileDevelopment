﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SidePageView : ContentPage
    {
        public SidePageView(SidePageViewModel sidePageViewModel)
        {
            InitializeComponent();
            BindingContext = sidePageViewModel;
        }
    }
}