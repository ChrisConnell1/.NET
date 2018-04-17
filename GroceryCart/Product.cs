using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCart
{
    class Product
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Subtotal;

        public double subtotal
        {
            get { return Price*Quantity; }
            set { Subtotal = value; }
        }

    }
}
