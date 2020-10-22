using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DeliveryApp.Model;

namespace DeliveryApp.Controller
{
    public sealed class ControllerSingleton
    {
        private CatalogController _catalogController;
        private static CartController _cartController;

        private ControllerSingleton()
        {
            _catalogController = new CatalogController();
            _cartController = new CartController();
        }

        private static readonly Lazy<ControllerSingleton> instance =
           new Lazy<ControllerSingleton>(() => new ControllerSingleton());

        public static ControllerSingleton Instance => instance.Value;

        public List<Product> GetProductsForCatalog()
        {
            return _catalogController.Products;
        }

        public void SetConfrimation()
        {
            _catalogController.Confirm();
        }

        public bool ConfirmProducts()
        {
            if (_catalogController.IsConfirming)
            {
                if (_catalogController.HasEmptyChosenProducts() && _cartController.HasEmptyCart())
                {
                    ReturnError();
                    return false;
                }
                else
                {
                    _cartController.UpdateCart(_catalogController.ChosenProducts);
                }
            }

            return true;
        }

        public void SetCatalogDataToDefault()
        {
            _catalogController.UpdateData();
        }

        public void UpdateProductsInCatalog(object product, int value)
        {
            if (product == null) return;

            _catalogController.UpdateProductsCountInCatalog(product, value);
        }

        public List<Product> GetProductsInCart()
        {
            return _cartController.GetCart();
        }

        public void DeleteProductInCart(Product product)
        {
            if (product == null) return;

            _cartController.DeleteProduct(product);
        }

        public bool CheckedOrder()
        {
            if (_cartController.HasEmptyCart())
            {
                ReturnError();
                return false;
            }

            return true;
        }

        public void CompleteOrder()
        {
            _cartController.EmptyCart();
        }

        private async void ReturnError()
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Choose at least one product", "OK");
        }
    }
}
