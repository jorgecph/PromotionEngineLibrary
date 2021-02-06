using PromotionEngineLibrary;
using System;
using System.Collections.Generic;

namespace PromotionEngineApp
{
    class Program
    {
        private static string desiredQuantityQuestion = "Please type the desired quantity: ";

        static void Main(string[] args)
        {
            var engine = Factory.GetNewEngine();
            var cart = Factory.GetNewCart();

            PopulateProducts(engine);
            AddPromotions(engine);

            ReadInputCartEntry(cart);

            Console.WriteLine();
            Console.WriteLine($"Your total price is {engine.CalculatePrice(cart)}M");
            Console.WriteLine(  );
        }

        private static void ReadInputCartEntry(Cart cart)
        {
            string eneteredMoreItems = String.Empty;
            do
            {
                Console.Write("Please type the Sku of the product: ");
                string sku = Console.ReadLine();

                Console.Write(desiredQuantityQuestion);
                string quantityString = Console.ReadLine();

                int quantity;
                while (Int32.TryParse(quantityString, out quantity) == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid number enetered.");
                    Console.Write(desiredQuantityQuestion);
                    quantityString = Console.ReadLine();
                }

                cart.AddItem(sku, quantity);

                Console.Write("Do you want to add another item (yes/no)?");
                eneteredMoreItems = Console.ReadLine();

            } while (eneteredMoreItems.Length > 0 && eneteredMoreItems.ToLower()[0].Equals('y'));
        }

        private static void PopulateProducts(Engine engine)
        {
            engine.AddProduct(Factory.GetNewProduct("A", 50));
            engine.AddProduct(Factory.GetNewProduct("B", 30));
            engine.AddProduct(Factory.GetNewProduct("C", 25));
            engine.AddProduct(Factory.GetNewProduct("D", 65));
            engine.AddProduct(Factory.GetNewProduct("E", 62));
            engine.AddProduct(Factory.GetNewProduct("F", 55));                        
        }

        private static void AddPromotions(Engine engine)
        {
            var productCandD = new List<IProduct>();
            var productDEF = new List<IProduct>();

            productCandD.Add(engine.GetProduct("C"));
            productCandD.Add(engine.GetProduct("D"));

            productDEF.Add(engine.GetProduct("D"));
            productDEF.Add(engine.GetProduct("E"));
            productDEF.Add(engine.GetProduct("F"));

            engine.AddPromotion(130M, 3, new List<IProduct>() { engine.GetProduct("A") } );
            engine.AddPromotion(100M, 2, new List<IProduct>() { engine.GetProduct("F") } );
            engine.AddPromotion(80M, productCandD );
            engine.AddPromotion(150M, productDEF );
        }
    }
}
