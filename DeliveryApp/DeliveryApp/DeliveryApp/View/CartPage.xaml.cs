using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DeliveryApp.Controller;
using DeliveryApp.Model;

namespace DeliveryApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            Appearing += CartPage_OnAppearing;
        }

        private void CartPage_OnAppearing(object sender, System.EventArgs e)
        {
            SetPageView();
        }

        private async void AddProductsButton_Clicked(object sender, System.EventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            await Navigation.PushAsync(new CatalogPage()); 

            ((Button)sender).IsEnabled = true;
        }

        private void DeleteProductButton_Clicked(object sender, System.EventArgs e)
        { 
            ControllerSingleton.Instance.DeleteProductInCart(((Button)sender).BindingContext as Product);
            SetPageView();
        }

        private async void OrderProducts_ButtonClicked(object sender, System.EventArgs e)
        {
            if (ControllerSingleton.Instance.CheckedOrder())
            {
                bool complitedOrder = await Application.Current.MainPage.DisplayAlert("Order",
                                            "Click ok to complete the order", "OK", "Cancel");

                if (complitedOrder)
                {
                    ControllerSingleton.Instance.CompleteOrder();
                    SetPageView();
                }
            }
        }

        private void SetPageView()
        {
            Cart.ItemsSource = ControllerSingleton.Instance.GetProductsInCart();
        }
    }
}