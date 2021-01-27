﻿using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public class Engine
    {
        private List<IPromotion> currentPromotions = new List<IPromotion>();
        private List<IProduct> products = new List<IProduct>();

        public void AddProduct(IProduct product)
        {
            products.Add(product);
        }

        public void AddPromotion(IPromotion promotion)
        {
            currentPromotions.Add(promotion);
        }

        public bool RemovePromotion(IPromotion promotion)
        {
            return currentPromotions.Remove(promotion);
        }

        public decimal CalculatePrice(Cart cart)
        {
            decimal output = 0M;
            PromotionEngine promotionEngine = new PromotionEngine(currentPromotions);

            output += cart.Contents.Count > 1 ? promotionEngine.ApplyPromotionInvolvingSeveralProducts(ref cart) : 0M;
            output += CalculateSingleProductPromotions(cart, promotionEngine);

            return output;
        }

        private decimal CalculateSingleProductPromotions(Cart cart, PromotionEngine promotionEngine)
        {
            decimal output = 0M;
            decimal promotionValue = 0;
            int missingItems;

            foreach (var item in cart.Contents)
            {
                if (item.Quantity == 0)
                {
                    continue;
                }

                promotionValue = promotionEngine.CalculateSimplePromotions(new List<IProduct> { products.Find(product => Equals(product.Sku, item.Sku)) }, item.Quantity, out missingItems);

                output += promotionValue + products.Find(product => Equals(product.Sku, item.Sku)).Price * missingItems;
            }

            return output;
        }
    }
}
