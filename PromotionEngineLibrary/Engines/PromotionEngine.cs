using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngineLibrary
{
    public class PromotionEngine : Engine
    {
        private Store store;

        public override decimal CalculatePrice(Cart cart, Store store)
        {
            Cart internalCart = cart.Copy();

            this.store = store;
            return GetPriceBasedOnPromotions(internalCart.Contents);
        }

        private decimal CalculateSimplePromotions(IProduct product, int quantity, out int missingItems)
        {
            decimal output = 0;
            missingItems = quantity;

            foreach (var promotion in store.Promotions)
            {
                if (promotion.InvolvedProducts.Count() != 1 || !promotion.InvolvedProducts[0].Sku.Equals(product.Sku))
                {
                    continue;
                }

                // TODO: Remove the while, and introduce the mathematical function below
                while (missingItems >= promotion.NumberOfProducts)
                {
                    missingItems -= promotion.NumberOfProducts;
                    if (promotion.Discount > 0)
                    {
                        output += product.Price * promotion.NumberOfProducts * ((100 - promotion.Discount) / 100);
                    }
                    else
                    {
                        output += promotion.Cost;
                    }
                }
            }

            return output;
        }

        private decimal ApplyMultipleProductPromotions(Dictionary<string, int> items)
        {
            Dictionary<string, int> involvedItems = new Dictionary<string, int>();
            decimal output = 0;

            store.Promotions.ForEach(delegate (IPromotion promotion)
            {
                if (promotion.InvolvedProducts.Count() > 1)
                {
                    promotion.InvolvedProducts.ForEach(delegate (IProduct involvedProduct)
                    {
                        if (items.TryGetValue(involvedProduct.Sku, out int quantity) && quantity > 0)
                        {
                            involvedItems.Add(involvedProduct.Sku, quantity);
                        }
                    });

                    if (promotion.InvolvedProducts.Count == involvedItems.Count)
                    {
                        while (involvedItems.ContainsValue(0) == false)
                        {
                            Utilities.DecrementQuantity(involvedItems, 1);
                            output += promotion.Cost;
                        }

                        Utilities.UpdateQuantities(items, involvedItems);
                    }

                    involvedItems.Clear();
                }
            });

            return output;
        }

        private decimal ApplySingleProductPromotions(Dictionary<string, int> items)
        {
            decimal output = 0M;
            int missingItems;

            foreach (var item in items)
            {
                if (item.Value == 0)
                {
                    continue;
                }

                try
                {
                    output += CalculateSimplePromotions(
                        store.Products.Find(product => Equals(product.Sku, item.Key)),
                        item.Value,
                        out missingItems) +
                    store.Products.Find(product => Equals(product.Sku, item.Key)).Price * missingItems;

                }
                catch (NullReferenceException)
                {
                    throw new Exception($"Item {item.Key} with {item.Value} items could not be processed.");
                }            
            }

            return output;
        }

        public decimal GetPriceBasedOnPromotions(Dictionary<string, int> items)
        {
            decimal output = 0M;
            output += items.Count > 1 ? ApplyMultipleProductPromotions(items) : 0M;
            output += ApplySingleProductPromotions(items);
            return output;
        }
    }
}
