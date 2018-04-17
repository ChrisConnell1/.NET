using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCart
{
    class Cart
    {
        public List<Product> cartProducts { get; set; }

        public void AddToCart(Product product) {
            cartProducts.Add(product);
            product.Quantity++;
        }

        public void DeleteFromCart(Product product)
        {
            cartProducts.Remove(product);
            product.Quantity--;
        }

        public void ClearCart()
        {
            cartProducts.Clear();
        }

        public void SortByName()
        {
            cartProducts.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        public void SortByPrice()
        {
            cartProducts.Sort((x, y) => x.subtotal.CompareTo(y.subtotal));
        }
    }
}
