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

        private decimal CalculateSingleProductPromotions(IProduct product, int quantity)
        {
            decimal output = 0;

            foreach (var promotion in store.SingleProductPromotions.FindAll(p => p.InvolvedProducts[0].Sku.Equals(product.Sku)))
            {
                int numberOfGroups = quantity / promotion.NumberOfProducts;
                if (promotion.Discount > 0)
                {
                    output += numberOfGroups * product.Price * promotion.NumberOfProducts * ((100 - promotion.Discount) / 100);
                }
                else
                {
                    output += numberOfGroups * promotion.Cost;
                }

                quantity -= promotion.NumberOfProducts * numberOfGroups;
            }

            output += product.Price * quantity;

            return output;
        }

        private decimal CalculateMultipleProductPromotions(Dictionary<string, int> items)
        {
            Dictionary<string, int> involvedItems = new Dictionary<string, int>();
            decimal output = 0;

            foreach (var promotion in store.MultipleProductPromotions)
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

            return output;
        }

        private decimal ApplySingleProductPromotions(Dictionary<string, int> items)
        {
            decimal output = 0M;

            foreach (var item in items.Where(i => i.Value != 0))
            {
                try
                {
                    output += CalculateSingleProductPromotions(
                        store.Products.Find(product => Equals(product.Sku, item.Key)),
                        item.Value);
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
            output += items.Count > 1 ? CalculateMultipleProductPromotions(items) : 0M;
            output += ApplySingleProductPromotions(items);
            return output;
        }
    }
}
