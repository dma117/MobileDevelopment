using System.Collections.Generic;
using DeliveryApp.Model;

namespace DeliveryApp.Controller
{
    class CatalogController
    {
        private static readonly List<Product> _products;
        private static List<Product> _chosenProducts;
        private static bool _isConfirming;
        static CatalogController()
        {
           _products = new List<Product>
           {
                { new Product("Water", "Water.png")},
                { new Product("Juice", "Juice.png")},
                { new Product("CocaCola", "CocaCola.png")}
           };

            _chosenProducts = new List<Product>();
            _isConfirming = false;
        }

        public List<Product> Products => _products;
        public List<Product> ChosenProducts => _chosenProducts;
        public bool IsConfirming => _isConfirming;

        public void UpdateProductsCountInCatalog(object product, int value)
        {
            (product as Product).Count = value;
            UpdateChosenProducts(product as Product);
        }

        public void Confirm()
        {
            _isConfirming = true;
        }

        public bool HasEmptyChosenProducts()
        {
            return _chosenProducts.Count == 0;
        }

        public void UpdateData()
        {
            foreach (var product in _chosenProducts)
            {
                product.Count = 0;
            }

            _chosenProducts.Clear();
            _isConfirming = false;
        }

        private void UpdateChosenProducts(Product product)
        {
            if (product.Count == 0)
            {
                _chosenProducts.Remove(product);
            }
            else
            {
                if (!_chosenProducts.Contains(product))
                {
                    _chosenProducts.Add(product);
                }
            }
        }
    }
}
