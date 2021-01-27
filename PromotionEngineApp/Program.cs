using PromotionEngineLibrary;
using System;
using System.Collections.Generic;

namespace PromotionEngineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            Cart cart = new Cart();

            engine.CurrentPromotions.Add(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            engine.AddProduct(new Product("A", 50));
            cart.AddItem("A", 3);

            Console.WriteLine($"{engine.CalculatePrice(cart)}");
            Console.WriteLine(  );
        }
    }
}
