using System;
using System.ComponentModel;

namespace DeliveryApp.Model
{
    public class Product : INotifyPropertyChanged, ICloneable, IEquatable<Product>
    {
        private readonly string _name;
        private readonly string _url;
        private int _id;
        private int _count;
        private static int _productsCount;

        public Product(string name, string url)
        {
            _name = name;
            _url = url;
            _id = _productsCount++;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Equals(Product other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        /*public override int GetHashCode()
        {
            return (this.TagId != null ? this.TagId.GetHashCode() : 0);
        }*/

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name => _name;
        public int Id => _id;
        public string ImageUrl => _url;
        public int Count
        {
            get => _count;

            set
            {
                if (value != _count)
                {
                    _count = value;



                    OnPropertyChanged("Count");
                }
            }
        }

        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
 