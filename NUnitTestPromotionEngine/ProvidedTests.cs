using NUnit.Framework;
using PromotionEngineLibrary;
using System.Collections.Generic;

namespace NUnitTestPromotionEngine
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPromotion_3_A_for_150()
        {
            Engine engine = new Engine();
            Cart cart = new Cart();

            engine.CurrentPromotions.Add(new Promotion() { Cost = 130M, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            cart.AddProduct(new Product("A", 50), 3);

            Assert.Equals(engine.CalculatePrice(cart), 130M);
        }
    }
}