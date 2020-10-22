using System.Collections.Generic;
using DeliveryApp.Model;

namespace DeliveryApp.Controller
{
    class CartController
    {
        private List<Product> _cart;

        public CartController()
        {
            _cart = new List<Product>();
        }

        public List<Product> GetCart()
        {
            return new List<Product>(_cart);
        }

        public bool HasEmptyCart()
        {
            return _cart.Count == 0;
        }

        public void UpdateCart(List<Product> products)
        {
            foreach (var element in products)
            {
                if (_cart.Contains(element))
                {
                    _cart[_cart.IndexOf(element)].Count += element.Count;
                } 
                else
                {
                    _cart.Add((Product)element.Clone());
                }
            }
        }

        public void DeleteProduct(Product product)
        {
            _cart.Remove(product);
        }

        public void EmptyCart()
        {
            _cart.Clear();
        }
    }
}
