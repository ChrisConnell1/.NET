using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCart
{
    class Program
    {

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            Cart cart = new Cart() {cartProducts = new List<Product>() };

            Inventory store = new Inventory();
            var storeProducts = store.storeInventory;

            Console.Clear();
            Console.WriteLine("Welcome to Chris' shop! Please select an option below: \n\n");
            Console.WriteLine("Enter 'P' or 'Products' to view our product line. \n");
            Console.WriteLine("Enter 'C' or 'Cart' to view your cart \n");
            Console.WriteLine("Enter 'Q' to quit.\n");
            Console.WriteLine("\n\n\n");


            string mainInput = Console.ReadLine();
            mainInput = mainInput.ToUpper();


            if (mainInput == "P" || mainInput == "PRODUCTS")
                ProductsMenu(cart, storeProducts);
            else if (mainInput == "C" || mainInput == "CART")
                CartView(cart, storeProducts);
            else if (mainInput == "Q")
                Environment.Exit(0);
        }

        private static void CartView(Cart cart, List<Product> storeProducts)
        {
            Console.Clear();
            double total = 0.00;

            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            Console.WriteLine("Here is your cart: \n");

            //Check if cart is empty
            if (cart.cartProducts.Count == 0)
            {

                Console.WriteLine("You have no items in your cart! Please view our products to add some.\n");
                Console.WriteLine("Press any key to return");

                Console.ReadLine();
                ProductsMenu(cart, storeProducts);
            }

            //If not empty, display list of products.
            else
            { 

                for (int i = 0; i < cart.cartProducts.Count; i++)
                {
                    Product product = cart.cartProducts.ElementAt(i);

                    Console.WriteLine("{0}  -   {1} ({2}) at {3} for a subtotal of {4} \n\n", product.Name, product.Quantity, product.Unit,
                        product.Price, product.subtotal);

                    total += product.subtotal;
                }

                Console.WriteLine("Your current total is: {0:C}\n\n", total);

                Console.WriteLine("Cart options: \n\nEnter 'subtotal' to sort by subtotal.\nEnter 'name' to sort by name.\n\nEnter 'products' to return to product menu.\n");
                Console.WriteLine("Delete item: Enter the product name. (i.e Banana)\n");
                Console.WriteLine("To clear your cart, enter 'clear'");
                Console.WriteLine("To checkout, please type 'check'.\n\n");

                string cartInput = Console.ReadLine();
                cartInput = myTI.ToTitleCase(cartInput);

                //Could implement these in an array. Might be faster with larger dataset.
                if (cartInput == "Bread" || cartInput == "Milk" || cartInput == "Orange" || cartInput == "Banana")
                {

                    if (cart.cartProducts.Exists(i => i.Name == cartInput))
                    {
                        SendToDeleteCart(cart.cartProducts.ElementAt(cart.cartProducts.FindIndex(i => i.Name == cartInput)), cart, storeProducts);
                    }

                    else Console.WriteLine("You don't have any of those!");
                    Console.ReadLine();
                    CartView(cart, storeProducts);
                }


                else
                {
                    switch (cartInput)
                    {
                        case "Name":
                            cart.SortByName();
                            CartView(cart, storeProducts);
                            break;
                        case "Subtotal":
                            cart.SortByPrice();
                            CartView(cart, storeProducts);
                            break;
                        case "Check":
                            Checkout(cart, total);
                            CartView(cart, storeProducts);
                            break;
                        case "Clear":
                            cart.ClearCart();
                            CartView(cart, storeProducts);
                            break;
                        case "Products":
                            ProductsMenu(cart, storeProducts);
                            break;
                        default:
                            CartView(cart, storeProducts);
                            break;
                       }
                    }

           
                }
                
            }

        static void ProductsMenu(Cart cart, List<Product> storeProducts)
        {
            //Products will have quantity of 0 until in cart.
            Console.Clear();
            Console.WriteLine("Here are the products we offer, please have a look. \n \n");

            //Formatting menu
            Console.WriteLine("   Item    Unit     Price \n \n");
            for (int i = 0; i < storeProducts.Count; i++)
            {
                Console.WriteLine("{0}. {1} - {2}      {3:C}\n\n", i+1, storeProducts.ElementAt(i).Name, storeProducts.ElementAt(i).Unit, storeProducts.ElementAt(i).Price);
            }
            


            Console.WriteLine("If you are interested, select the corresponding number to add product to your cart. (e.g 1 to add bananas.)\n\n");
            Console.WriteLine("Enter 'cart' to view cart.\n\n");
            string productInput = Console.ReadLine();
            productInput = productInput.ToUpper();

            switch (productInput)
            {
                case "1":
                    SendToAddCart(storeProducts.ElementAt(0), cart, storeProducts);
                    break;
                case "2":
                    SendToAddCart(storeProducts.ElementAt(1), cart, storeProducts);
                    break;
                case "3":
                    SendToAddCart(storeProducts.ElementAt(2), cart, storeProducts);
                    break;
                case "4":
                    SendToAddCart(storeProducts.ElementAt(3), cart, storeProducts);
                    break;
                case "CART":
                    CartView(cart, storeProducts);
                    break;
                default:
                    ProductsMenu(cart, storeProducts);
                    break;
            }
        }

        private static void SendToAddCart(Product product, Cart cart, List<Product> storeProducts)
        {

            // First see if item already exists in cart. If not, add it. If it does, just add to quantity.
            if (!cart.cartProducts.Any(item => item.Name == product.Name))
            { 
                cart.AddToCart(product);
                CartView(cart, storeProducts);
            }

            else
            {
                product.Quantity++;
                CartView(cart, storeProducts);
            }
        }

        private static void SendToDeleteCart (Product product, Cart cart, List<Product> storeProducts)
        {
            //If there exists more than one of the type in cart, don't delete product from cart, just decrement quantity.
            if (product.Quantity > 1)
            {
                product.Quantity--;
                CartView(cart, storeProducts);
            }

            else cart.DeleteFromCart(product);

            CartView(cart, storeProducts);
        }

        private static void Checkout (Cart cart, double total)
        {
            Console.Clear();
            Console.WriteLine("Checkout: Your total is {0:C}\nType 'return' to add more items, or continue below.\n\n", total);
            Console.WriteLine("We accept payments of check or debit/credit.\n");

            Console.WriteLine("Enter 'check', 'card', or 'return'.\n");
            Console.ReadLine();

            string checkoutInput = Console.ReadLine();

            switch (checkoutInput)
            {
                case "return":
                   break;

                case "check":
                    Console.WriteLine("Enter your full name: \n");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter your routing number: \n");
                    string routing = Console.ReadLine();
                    Console.WriteLine("Enter your account number: \n");
                    string account = Console.ReadLine();
                    Console.WriteLine("Thank you {0}! Your account will be billed for {1:C}", name, total);
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;

                case "card":
                    Console.WriteLine("Enter name as it appears on card: \n\n");
                    string cardName = Console.ReadLine();
                    Console.WriteLine("Enter full card number: \n\n");
                    string cardNumber = Console.ReadLine();
                    Console.WriteLine("Enter expiration date (mm/yy): \n\n");
                    string expDate = Console.ReadLine();
                    Console.WriteLine("Enter full billing address: (123 N St. Portland, OR 97221\n\n");
                    string address = Console.ReadLine();
                    Console.WriteLine("Thank you for your purchase, {0}! Your card number {1} will be billed for {2:C}.", cardName, cardNumber, total);
                    Console.Write("Expect delivery within 2-3 days to address: {0}", address);
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;

                default: Checkout(cart, total);
                    break;
            }
        }
    }
}
