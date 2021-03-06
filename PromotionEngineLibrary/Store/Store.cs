﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class Store : IStore
    {
        public List<IProduct> Products { get; private set; } = new List<IProduct>();
        public List<DiscountPromotion> DiscountPromotions { get; private set; } = new List<DiscountPromotion>();

        private List<IPromotion> promotions = new List<IPromotion>();
        private List<IPromotion> singleProductPromotions;
        private List<IPromotion> multipleProductPromotions;

        public List<IPromotion> SingleProductPromotions
        {
            get
            {
                if (singleProductPromotions == null)
                {
                    singleProductPromotions = new List<IPromotion>();
                    singleProductPromotions.AddRange(promotions.FindAll(p => p.InvolvedProducts.Count() == 1));
                }

                return singleProductPromotions;
            }
        }

        public List<IPromotion> MultipleProductPromotions
        {
            get
            {
                if (multipleProductPromotions == null)
                {
                    multipleProductPromotions = new List<IPromotion>();
                    multipleProductPromotions.AddRange(promotions.FindAll(p => p.InvolvedProducts.Count() > 1));
                }

                return multipleProductPromotions;
            }
        }

        public void AddProduct(IProduct product)
        {
            Products.Add(product);
        }

        private void StatePromotionsHaveChanged()
        {
            singleProductPromotions = null;
            multipleProductPromotions = null;
        }

        public void AddPromotion(IPromotion promotion)
        {
            promotions.Add(promotion);
            StatePromotionsHaveChanged();
        }

        public void AddPromotion(decimal cost, int numberOfProducts, List<IProduct> involvedProducts)
        {
            AddPromotion(new Promotion() { Cost = cost, NumberOfProducts = numberOfProducts, InvolvedProducts = involvedProducts });
        }

        public void AddPromotion(int discount, int numberOfProducts, List<IProduct> involvedProducts)
        {
            AddPromotion(new Promotion() { Discount = discount, NumberOfProducts = numberOfProducts, InvolvedProducts = involvedProducts });
        }

        public void AddPromotion(decimal cost, List<IProduct> involvedProducts)
        {
            AddPromotion(new Promotion() { Cost = cost, InvolvedProducts = involvedProducts });
        }

        public bool RemovePromotion(IPromotion promotion)
        {
            StatePromotionsHaveChanged();
            return promotions.Remove(promotion);
        }

        public IProduct GetProduct(string sku)
        {
            return Products.Find(p => p.Sku.Equals(sku));
        }
    }
}
