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

            engine.CurrentPromotions.Add(new Promotion() { Cost = 130M, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            cart.AddProduct(new Product("A", 50), 3);

            Console.WriteLine($"{engine.CalculatePrice(cart)}");
            Console.WriteLine(  );
        }
    }
}
