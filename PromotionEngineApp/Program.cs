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
            var cart = Factory.CreateCart();
            var store = Factory.CreateStore();

            PopulateProducts(store);
            AddPromotions(store);
            AddDiscountPromotions(store);

            ReadInputCartEntry(cart);

            cart.SetPromotionStrategy(Factory.CreatePromotionEngine());
            Console.WriteLine();
            Console.WriteLine($"Your total price is {cart.CalculatePrice(store).ToString("C")}");
            Console.WriteLine(  );
        }

        private static void ReadInputCartEntry(ICart cart)
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

        private static void PopulateProducts(Store store)
        {
            store.AddProduct(Factory.CreateProduct("A", 50));
            store.AddProduct(Factory.CreateProduct("B", 30));
            store.AddProduct(Factory.CreateProduct("C", 25));
            store.AddProduct(Factory.CreateProduct("D", 65));
            store.AddProduct(Factory.CreateProduct("E", 62));
            store.AddProduct(Factory.CreateProduct("F", 55));                        
        }

        private static void AddPromotions(Store store)
        {
            var productCandD = new List<IProduct>();
            var productDEF = new List<IProduct>();

            productCandD.Add(store.GetProduct("C"));
            productCandD.Add(store.GetProduct("D"));

            productDEF.Add(store.GetProduct("D"));
            productDEF.Add(store.GetProduct("E"));
            productDEF.Add(store.GetProduct("F"));

            store.AddPromotion(130M, 3, new List<IProduct>() { store.GetProduct("A") } );
            store.AddPromotion(100M, 2, new List<IProduct>() { store.GetProduct("F") } );
            store.AddPromotion(80M, productCandD );
            store.AddPromotion(150M, productDEF );
        }

        private static void AddDiscountPromotions(Store store)
        {
            store.DiscountPromotions.Add(new MultipleItemDiscountPromotion(store));
            store.DiscountPromotions.Add(new SingleProductDiscountPromotion(store));
        }
    }
}
