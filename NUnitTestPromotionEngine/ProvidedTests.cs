using NUnit.Framework;
using PromotionEngineLibrary;
using System.Collections.Generic;

namespace NUnitTestPromotionEngine
{
    public class Tests
    {
        private Engine engine;
        private Cart cart;

        [SetUp]
        public void Setup()
        {
            cart = new Cart();

            engine = new Engine();
            engine.AddProduct(new Product("A", 50));
            engine.AddProduct(new Product("B", 30));
            engine.AddProduct(new Product("C", 20));
            engine.AddProduct(new Product("D", 15));
        }

        [Test]
        public void TestScenarioA()
        {
            cart.AddItem("A", 1);
            Assert.AreEqual(engine.CalculatePrice(cart), 50);

            cart.ClearItems();
            cart.AddItem("B", 1);
            Assert.AreEqual(engine.CalculatePrice(cart), 30);

            cart.ClearItems();
            cart.AddItem("C", 1);
            Assert.AreEqual(engine.CalculatePrice(cart), 20);
        }

        [Test]
        public void TestPromotion_3_A_for_150()
        {
            engine.CurrentPromotions.Add(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            cart.AddItem("A", 3);

            Assert.AreEqual(engine.CalculatePrice(cart), 130);
        }
    }
}