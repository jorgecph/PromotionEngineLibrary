using NUnit.Framework;
using PromotionEngineLibrary;
using System.Collections.Generic;

namespace NUnitTestPromotionEngine
{
    public class Tests
    {
        private Engine engine;
        private Cart cart;

        [OneTimeSetUp]
        public void Init()
        {
            cart = new Cart();
            engine = new Engine();
            engine.AddProduct(new Product("A", 50));
            engine.AddProduct(new Product("B", 30));
            engine.AddProduct(new Product("C", 20));
            engine.AddProduct(new Product("D", 15));

            engine.AddPromotion(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            engine.AddPromotion(new Promotion() { Cost = 45M, NumberOfProducts = 2, InvolvedProducts = new List<IProduct>() { new Product("B", 50) } });
        }

        [SetUp]
        public void Setup()
        {
            cart.ClearItems();
        }

        [Test]
        public void TestScenarioA()
        {
            cart.ClearItems();
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
        public void TestScenarioB()
        {
            cart.AddItem("A", 5);
            //cart.AddItem("B", 5);
            Assert.AreEqual(engine.CalculatePrice(cart), 230);
        }

        [Test]
        public void TestSimplePromotions()
        {
            engine.AddPromotion(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            cart.AddItem("A", 3);

            Assert.AreEqual(engine.CalculatePrice(cart), 130);
        }
    }
}