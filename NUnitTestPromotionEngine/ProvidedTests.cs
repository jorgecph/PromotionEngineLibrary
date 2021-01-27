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
            Cart cart = new Cart();
            cart.AddProduct(new Product("A", string.Empty, 50), 3);

            Assert.Equals(Engine.CalculatePrice(cart), 130M);
        }
    }
}