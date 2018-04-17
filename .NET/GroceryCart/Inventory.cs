using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCart
{
    class Inventory
    {

        //This class will be strictly for a list of the available products. Could be IEnum as well.
        public Inventory()
        {
                storeInventory = new List<Product>() {
                new Product() { Name = "Banana", Unit = "Bunch", Quantity = 0, Price = 2.49 },
                new Product() { Name = "Milk", Unit = "Gallon", Quantity = 0, Price = 1.49 },
                new Product() { Name = "Bread", Unit = "Loaf", Quantity = 0, Price = 2.99 },
                new Product() { Name = "Orange", Unit = "Each", Quantity = 0, Price = 0.89 }
                };
        }

        public List<Product> storeInventory { get; set; }

        
    }
}
