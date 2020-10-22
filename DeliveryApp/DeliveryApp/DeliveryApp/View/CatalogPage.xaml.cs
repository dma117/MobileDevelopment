using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeliveryApp.Controller;

namespace DeliveryApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogPage : ContentPage
    {
        public CatalogPage()
        {
            InitializeComponent();

            ProductsPicker.ItemsSource = ControllerSingleton.Instance.GetProductsForCatalog();
            SetStartingItemInPicker();
            Disappearing += CatalogPage_Disappearing;
        }

        private void CatalogPage_Disappearing(object sender, EventArgs e)
        {
            //CatalogController.UpdateData();
            ControllerSingleton.Instance.SetCatalogDataToDefault();
        }

        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            //CatalogController.Confirm();
            ControllerSingleton.Instance.SetConfrimation();

            if (ControllerSingleton.Instance.ConfirmProducts())
            {
                await Navigation.PopAsync();
            }
        }

        private void SetStartingItemInPicker()
        {
            ProductsPicker.SelectedItem = ProductsPicker.ItemsSource[0];
        }

        private void ProductsCountStepper_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            ControllerSingleton.Instance.UpdateProductsInCatalog(ProductsPicker.SelectedItem, (int)((Stepper)sender).Value);
        }

        private void ProductsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductsCountStepper.Value = Int32.Parse(ProductsCountEditor.Text);
        }
    }
}