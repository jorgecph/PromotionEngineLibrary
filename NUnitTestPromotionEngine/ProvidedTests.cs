using NUnit.Framework;
using PromotionEngineLibrary;
using System;
using System.Collections.Generic;

namespace NUnitTestPromotionEngine
{
    public class Tests
    {
        private ICart cart;
        private IStore store;

        [OneTimeSetUp]
        public void Init()
        {
            cart = Factory.CreateCart();
            store = Factory.CreateStore();

            PopulateProducts();
            AddPromotions();
            AddDiscountPromotions();
            cart.SetPromotionStrategy(Factory.CreatePromotionEngine());
        }

        private void AddDiscountPromotions()
        {
            store.DiscountPromotions.Add(new MultipleItemDiscountPromotion(store));
            store.DiscountPromotions.Add(new SingleProductDiscountPromotion(store));
        }

        private void PopulateProducts()
        {
            store.AddProduct(Factory.CreateProduct("A", 50));
            store.AddProduct(Factory.CreateProduct("B", 30));
            store.AddProduct(Factory.CreateProduct("C", 20));
            store.AddProduct(Factory.CreateProduct("D", 15));
            store.AddProduct(Factory.CreateProduct("E", 62));
            store.AddProduct(Factory.CreateProduct("F", 55));
        }

        private void PopulateMoreProducts()
        {
            store.AddProduct(Factory.CreateProduct("G", 70));
            store.AddProduct(Factory.CreateProduct("H", 71));
            store.AddProduct(Factory.CreateProduct("I", 72));
            store.AddProduct(Factory.CreateProduct("J", 73));
            store.AddProduct(Factory.CreateProduct("K", 75));
            store.AddProduct(Factory.CreateProduct("L", 76));
            store.AddProduct(Factory.CreateProduct("M", 77));
            store.AddProduct(Factory.CreateProduct("N", 78));
            store.AddProduct(Factory.CreateProduct("O", 79));
            store.AddProduct(Factory.CreateProduct("P", 80));
            store.AddProduct(Factory.CreateProduct("Q", 81));
            store.AddProduct(Factory.CreateProduct("R", 82));
            store.AddProduct(Factory.CreateProduct("S", 83));
            store.AddProduct(Factory.CreateProduct("T", 84));
            store.AddProduct(Factory.CreateProduct("U", 85));
            store.AddProduct(Factory.CreateProduct("V", 86));
            store.AddProduct(Factory.CreateProduct("W", 87));
            store.AddProduct(Factory.CreateProduct("X", 88));
            store.AddProduct(Factory.CreateProduct("Y", 89));
            store.AddProduct(Factory.CreateProduct("Z", 90));
        }

        private void AddPromotions()
        {
            var productCandD = new List<IProduct>();
            var productDEF = new List<IProduct>();

            productCandD.Add(store.GetProduct("C"));
            productCandD.Add(store.GetProduct("D"));

            productDEF.Add(store.GetProduct("D"));
            productDEF.Add(store.GetProduct("E"));
            productDEF.Add(store.GetProduct("F"));

            store.AddPromotion(150M, productDEF);
            store.AddPromotion(130M, 3, new List<IProduct>() { store.GetProduct("A") });
            store.AddPromotion(45M, 2, new List<IProduct>() { store.GetProduct("B") });
            store.AddPromotion(30M, productCandD);
        }

        private void AddMorePromotions()
        {
            var productsUV = new List<IProduct>();
            productsUV.Add(store.GetProduct("U"));
            productsUV.Add(store.GetProduct("V"));
            store.AddPromotion(160M, productsUV);

            var productsNO = new List<IProduct>();
            productsNO.Add(store.GetProduct("N"));
            productsNO.Add(store.GetProduct("O"));
            store.AddPromotion(140M, productsNO);

            var productsXYZ = new List<IProduct>();
            productsXYZ.Add(store.GetProduct("X"));
            productsXYZ.Add(store.GetProduct("Y"));
            productsXYZ.Add(store.GetProduct("Z"));
            store.AddPromotion(200M, productsXYZ);

            var productsQRS = new List<IProduct>();
            productsQRS.Add(store.GetProduct("Q"));
            productsQRS.Add(store.GetProduct("R"));
            productsQRS.Add(store.GetProduct("S"));
            store.AddPromotion(210M, productsQRS);

            var productsGH = new List<IProduct>();
            productsGH.Add(store.GetProduct("G"));
            productsGH.Add(store.GetProduct("H"));
            store.AddPromotion(130M, productsGH);


            store.AddPromotion(10, 2, new List<IProduct>() { store.GetProduct("G") });
            store.AddPromotion(15, 3, new List<IProduct>() { store.GetProduct("I") });
            store.AddPromotion(10, 2, new List<IProduct>() { store.GetProduct("K") });
            store.AddPromotion(15, 3, new List<IProduct>() { store.GetProduct("S") });
            store.AddPromotion(15, 2, new List<IProduct>() { store.GetProduct("T") });
            store.AddPromotion(20, 3, new List<IProduct>() { store.GetProduct("U") });
            store.AddPromotion(1, 2, new List<IProduct>() { store.GetProduct("V") });
            store.AddPromotion(1, 3, new List<IProduct>() { store.GetProduct("W") });
            store.AddPromotion(15, 2, new List<IProduct>() { store.GetProduct("X") });
            store.AddPromotion(25, 3, new List<IProduct>() { store.GetProduct("Y") });
            store.AddPromotion(20, 2, new List<IProduct>() { store.GetProduct("Z") });
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
            Assert.AreEqual(50, cart.CalculatePrice(store));

            cart.ClearItems();
            cart.AddItem("B", 1);
            Assert.AreEqual(30, cart.CalculatePrice(store));

            cart.ClearItems();
            cart.AddItem("C", 1);
            Assert.AreEqual(20, cart.CalculatePrice(store));
        }

        [Test]
        public void TestScenarioB()
        {
            cart.AddItem("A", 5);
            cart.AddItem("B", 5);
            cart.AddItem("C", 1);
            Assert.AreEqual(370, cart.CalculatePrice(store));
        }

        [Test]
        public void TestScenarioC()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 1);
            cart.AddItem("D", 1);
            Assert.AreEqual(280, cart.CalculatePrice(store));
        }

        [Test]
        public void TestScenarioC2()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 2);
            cart.AddItem("D", 1);
            Assert.AreEqual(300, cart.CalculatePrice(store));
        }

        [Test]
        public void TestScenarioC3()
        {
            cart.AddItem("A", 3);
            cart.AddItem("B", 5);
            cart.AddItem("C", 2);
            cart.AddItem("D", 2);
            Assert.AreEqual(310, cart.CalculatePrice(store));
        }

        [Test]
        public void TestSimplePromotions()
        {
            store.AddPromotion(new Promotion() { Cost = 130M, NumberOfProducts = 3, InvolvedProducts = new List<IProduct>() { new Product("A", 50) } });
            cart.AddItem("A", 3);

            Assert.AreEqual(130, cart.CalculatePrice(store));
        }

        [Test]
        public void TestDoubleMultiplePromotions()
        {
            cart.AddItem("A", 3);
            cart.AddItem("C", 3);
            cart.AddItem("D", 4);
            cart.AddItem("E", 2);
            cart.AddItem("F", 1);

            Assert.AreEqual(432, cart.CalculatePrice(store));
        }

        [Test]
        public void TestDiscountPromotions()
        {
            PopulateMoreProducts();
            AddMorePromotions();

            cart.AddItem("U", 7);
            Assert.AreEqual((85 * 6 * 0.8 + 85), cart.CalculatePrice(store));

            cart.ClearItems();

            cart.AddItem("W", 11);
            Assert.AreEqual((87 * 9 * 0.99 + 87 * 2), cart.CalculatePrice(store));

            cart.AddItem("A", 5);

            Assert.AreEqual((87 * 9 * 0.99 + 87 * 2) + (130 + 2 * 50), cart.CalculatePrice(store));
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

            Assert.AreEqual(6951.19, cart.CalculatePrice(store));
        }
    }
}