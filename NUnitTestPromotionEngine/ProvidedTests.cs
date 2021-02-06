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
            cart = Factory.GetNewCart();
            engine = Factory.GetNewEngine();

            PopulateProducts();
            AddPromotions();
        }

        private void PopulateProducts()
        {
            engine.AddProduct(Factory.GetNewProduct("A", 50));
            engine.AddProduct(Factory.GetNewProduct("B", 30));
            engine.AddProduct(Factory.GetNewProduct("C", 20));
            engine.AddProduct(Factory.GetNewProduct("D", 15));
            engine.AddProduct(Factory.GetNewProduct("E", 62));
            engine.AddProduct(Factory.GetNewProduct("F", 55));
        }

        private void AddPromotions()
        {
            var productCandD = new List<IProduct>();
            var productDEF = new List<IProduct>();

            productCandD.Add(engine.GetProduct("C"));
            productCandD.Add(engine.GetProduct("D"));

            productDEF.Add(engine.GetProduct("D"));
            productDEF.Add(engine.GetProduct("E"));
            productDEF.Add(engine.GetProduct("F"));

            engine.AddPromotion(150M, productDEF);
            engine.AddPromotion(130M, 3, new List<IProduct>() { engine.GetProduct("A") });
            engine.AddPromotion(45M, 2, new List<IProduct>() { engine.GetProduct("B") });
            engine.AddPromotion(30M, productCandD);
        }


        [SetUp]
        public void Setup()
        {
            cart.ClearItems();
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
        public void TestScenarioB()
        {
            cart.AddItem("A", 5);
            cart.AddItem("B", 5);
            cart.AddItem("C", 1);
            Assert.AreEqual(engine.CalculatePrice(cart), 370);
        }

        [Test]
        public void TestScenarioC()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 1);
            cart.AddItem("D", 1);
            Assert.AreEqual(engine.CalculatePrice(cart), 280);
        }

        [Test]
        public void TestScenarioC2()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 2);
            cart.AddItem("D", 1);
            Assert.AreEqual(engine.CalculatePrice(cart), 300);
        }

        [Test]
        public void TestScenarioC3()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 2);
            cart.AddItem("D", 2);
            Assert.AreEqual(engine.CalculatePrice(cart), 310);
        }

        [Test]
        public void TestSimplePromotions()
        {
            engine.AddPromotion(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            cart.AddItem("A", 3);

            Assert.AreEqual(engine.CalculatePrice(cart), 130);
        }

        [Test]
        public void TestDoubleMultiplePromotions()
        {
            cart.AddItem("A", 3);
            cart.AddItem("C", 3);
            cart.AddItem("D", 4);
            cart.AddItem("E", 2);
            cart.AddItem("F", 1);

            Assert.AreEqual(engine.CalculatePrice(cart), 432);
        }
}
}