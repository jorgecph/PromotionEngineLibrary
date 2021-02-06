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

        private void PopulateMoreProducts()
        {
            engine.AddProduct(Factory.GetNewProduct("G", 70));
            engine.AddProduct(Factory.GetNewProduct("H", 71));
            engine.AddProduct(Factory.GetNewProduct("I", 72));
            engine.AddProduct(Factory.GetNewProduct("J", 73));
            engine.AddProduct(Factory.GetNewProduct("K", 75));
            engine.AddProduct(Factory.GetNewProduct("L", 76));
            engine.AddProduct(Factory.GetNewProduct("M", 77));
            engine.AddProduct(Factory.GetNewProduct("N", 78));
            engine.AddProduct(Factory.GetNewProduct("O", 79));
            engine.AddProduct(Factory.GetNewProduct("P", 80));
            engine.AddProduct(Factory.GetNewProduct("Q", 81));
            engine.AddProduct(Factory.GetNewProduct("R", 82));
            engine.AddProduct(Factory.GetNewProduct("S", 83));
            engine.AddProduct(Factory.GetNewProduct("T", 84));
            engine.AddProduct(Factory.GetNewProduct("U", 85));
            engine.AddProduct(Factory.GetNewProduct("V", 86));
            engine.AddProduct(Factory.GetNewProduct("W", 87));
            engine.AddProduct(Factory.GetNewProduct("X", 88));
            engine.AddProduct(Factory.GetNewProduct("Y", 89));
            engine.AddProduct(Factory.GetNewProduct("Z", 90));
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

        private void AddMorePromotions()
        {
            var productsUV = new List<IProduct>();
            productsUV.Add(engine.GetProduct("U"));
            productsUV.Add(engine.GetProduct("V"));
            engine.AddPromotion(160M, productsUV);

            var productsNO = new List<IProduct>();
            productsNO.Add(engine.GetProduct("N"));
            productsNO.Add(engine.GetProduct("O"));
            engine.AddPromotion(140M, productsNO);

            var productsXYZ = new List<IProduct>();
            productsXYZ.Add(engine.GetProduct("X"));
            productsXYZ.Add(engine.GetProduct("Y"));
            productsXYZ.Add(engine.GetProduct("Z"));
            engine.AddPromotion(200M, productsXYZ);

            var productsQRS = new List<IProduct>();
            productsQRS.Add(engine.GetProduct("Q"));
            productsQRS.Add(engine.GetProduct("R"));
            productsQRS.Add(engine.GetProduct("S"));
            engine.AddPromotion(210M, productsQRS);

            var productsGH = new List<IProduct>();
            productsGH.Add(engine.GetProduct("G"));
            productsGH.Add(engine.GetProduct("H"));
            engine.AddPromotion(130M, productsGH);


            engine.AddPromotion(10, 2, new List<IProduct>() { engine.GetProduct("G") });
            engine.AddPromotion(15, 3, new List<IProduct>() { engine.GetProduct("I") });
            engine.AddPromotion(10, 2, new List<IProduct>() { engine.GetProduct("K") });
            engine.AddPromotion(15, 3, new List<IProduct>() { engine.GetProduct("S") });
            engine.AddPromotion(15, 2, new List<IProduct>() { engine.GetProduct("T") });
            engine.AddPromotion(20, 3, new List<IProduct>() { engine.GetProduct("U") });
            engine.AddPromotion(1, 2, new List<IProduct>() { engine.GetProduct("V") });
            engine.AddPromotion(1, 3, new List<IProduct>() { engine.GetProduct("W") });
            engine.AddPromotion(15, 2, new List<IProduct>() { engine.GetProduct("X") });
            engine.AddPromotion(25, 3, new List<IProduct>() { engine.GetProduct("Y") });
            engine.AddPromotion(20, 2, new List<IProduct>() { engine.GetProduct("Z") });
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
            Assert.AreEqual(50, engine.CalculatePrice(cart));

            cart.ClearItems();
            cart.AddItem("B", 1);
            Assert.AreEqual(30, engine.CalculatePrice(cart));

            cart.ClearItems();
            cart.AddItem("C", 1);
            Assert.AreEqual(20, engine.CalculatePrice(cart));
        }

        [Test]
        public void TestScenarioB()
        {
            cart.AddItem("A", 5);
            cart.AddItem("B", 5);
            cart.AddItem("C", 1);
            Assert.AreEqual(370, engine.CalculatePrice(cart));
        }

        [Test]
        public void TestScenarioC()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 1);
            cart.AddItem("D", 1);
            Assert.AreEqual(280, engine.CalculatePrice(cart));
        }

        [Test]
        public void TestScenarioC2()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 2);
            cart.AddItem("D", 1);
            Assert.AreEqual(300, engine.CalculatePrice(cart));
        }

        [Test]
        public void TestScenarioC3()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 2);
            cart.AddItem("D", 2);
            Assert.AreEqual(310, engine.CalculatePrice(cart));
        }

        [Test]
        public void TestSimplePromotions()
        {
            engine.AddPromotion(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            cart.AddItem("A", 3);

            Assert.AreEqual(130, engine.CalculatePrice(cart));
        }

        [Test]
        public void TestDoubleMultiplePromotions()
        {
            cart.AddItem("A", 3);
            cart.AddItem("C", 3);
            cart.AddItem("D", 4);
            cart.AddItem("E", 2);
            cart.AddItem("F", 1);

            Assert.AreEqual(432, engine.CalculatePrice(cart));
        }

        [Test]
        public void TestDiscountPromotions()
        {
            PopulateMoreProducts();
            AddMorePromotions();

            cart.AddItem("U", 7);
            Assert.AreEqual((85 * 6 * 0.8 + 85), engine.CalculatePrice(cart));

            cart.ClearItems();

            cart.AddItem("W", 11);
            Assert.AreEqual((87 * 9 * 0.99 + 87 * 2), engine.CalculatePrice(cart));

            cart.AddItem("A", 5);
            Assert.AreEqual((87 * 9 * 0.99 + 87 * 2) + (130 + 2 * 50), engine.CalculatePrice(cart));
        }

        [Test]
        public void TestLotsOfPromotions()
        {
            PopulateMoreProducts();
            AddMorePromotions();

            cart.AddItem("A", 3);
            cart.AddItem("C", 3);
            cart.AddItem("D", 4);
            cart.AddItem("E", 2);
            cart.AddItem("F", 1);
            cart.AddItem("G", 5);
            cart.AddItem("H", 3);
            cart.AddItem("I", 6);
            cart.AddItem("J", 4);
            cart.AddItem("K", 5);
            cart.AddItem("L", 3);
            cart.AddItem("M", 8);
            cart.AddItem("N", 6);
            cart.AddItem("O", 5);
            cart.AddItem("P", 4);
            cart.AddItem("Q", 7);
            cart.AddItem("R", 6);
            cart.AddItem("S", 5);
            cart.AddItem("T", 4);
            cart.AddItem("U", 7);
            cart.AddItem("V", 6);
            cart.AddItem("W", 5);

            Assert.AreEqual(6951.19, engine.CalculatePrice(cart));
        }

    }
}