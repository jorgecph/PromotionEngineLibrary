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

            Product productA = new Product("A", 50);
            Product productB = new Product("B", 30);
            Product productC = new Product("C", 25);
            Product productD = new Product("D", 65);
            Product productE = new Product("E", 62);
            Product productF = new Product("F", 55);

            List<IProduct> productCandD = new();
            productCandD.Add(productC);
            productCandD.Add(productD);

            List<IProduct> productDEF = new();
            productDEF.Add(productD);
            productDEF.Add(productE);
            productDEF.Add(productF);


            engine.AddPromotion(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { productA } });
            engine.AddPromotion(new Promotion() { Cost = 100M, NumberOfProducts = 2, InvolvedProducts = new List<IProduct>() { productF } });
            engine.AddPromotion(new Promotion() { Cost = 80M, InvolvedProducts = productCandD });
            engine.AddPromotion(new Promotion() { Cost = 150M, InvolvedProducts = productDEF });

            engine.AddProduct(productA);
            engine.AddProduct(productB);
            engine.AddProduct(productC);
            engine.AddProduct(productD);
            engine.AddProduct(productE);
            engine.AddProduct(productF);
            
            cart.AddItem("A", 3);
            cart.AddItem("C", 3);
            cart.AddItem("D", 4);
            cart.AddItem("E", 1);
            cart.AddItem("F", 1);

            Console.WriteLine($"{engine.CalculatePrice(cart)}");
            Console.WriteLine(  );
        }
    }
}
