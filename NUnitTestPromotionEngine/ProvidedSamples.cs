using NUnit.Framework;
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
            List<Product, int> products = new List<Product>();

            Promotion current = new Promotion(product, 3, 130);

            Cart cart = new Cart(products);

            Assert.Equals(Engine.CalculatePrice(cart), 130M);
        }
    }
}