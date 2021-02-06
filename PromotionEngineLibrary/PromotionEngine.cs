using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngineLibrary
{
    internal class PromotionEngine
    {
        private readonly List<IPromotion> promotions;
        private readonly List<IProduct> products;

        public PromotionEngine(List<IPromotion> existingPromotions, List<IProduct> existingproducts)
        {
            promotions = existingPromotions;
            products = existingproducts;
        }

        private decimal CalculateSimplePromotions(List<IProduct> products, int quantity, out int missingItems)
        {
            decimal output = 0;
            missingItems = quantity;

            foreach (var promotion in promotions)
            {
                if (promotion.InvolvedProducts.Count() == 1 && promotion.InvolvedProducts.Except(products).Count() == 0)
                {
                    while (missingItems >= promotion.NumberOfProducts)
                    {
                        missingItems -= promotion.NumberOfProducts;
                        output += promotion.Cost;
                    }
                }
            }

            return output;
        }

        private decimal ApplyMultipleProductPromotions(List<ItemCart> items)
        {
            List<ItemCart> involvedItems = new List<ItemCart>();
            decimal output = 0;

            promotions.ForEach(delegate (IPromotion promotion)
            {
                if (promotion.InvolvedProducts.Count() > 1)
                {
                    promotion.InvolvedProducts.ForEach(delegate (IProduct involvedProduct)
                    {
                        ItemCart foundItem = items.Find(i => Equals(i.Sku, involvedProduct.Sku));

                        if (foundItem.Quantity > 0)
                        {
                            involvedItems.Add(foundItem);
                        }
                    });

                    if (promotion.InvolvedProducts.Count == involvedItems.Count)
                    {
                        while (!involvedItems.Exists(i => i.Quantity == 0))
                        {
                            involvedItems = Utilities.DecrementQuantity(involvedItems);
                            output += promotion.Cost;
                        }
                    }

                    Utilities.ReplaceItems(items, involvedItems);
                    involvedItems.Clear();
                }
            });

            return output;
        }

        private decimal ApplySingleProductPromotions(List<ItemCart> items)
        {
            decimal output = 0M;
            int missingItems;

            foreach (var item in items)
            {
                if (item.Quantity == 0)
                {
                    continue;
                }

                try
                {
                    output += CalculateSimplePromotions(
                        new List<IProduct> { products.Find(product => Equals(product.Sku, item.Sku)) },
                        item.Quantity,
                        out missingItems) +
                    products.Find(product => Equals(product.Sku, item.Sku)).Price * missingItems;

                }
                catch (NullReferenceException e)
                {
                    throw new Exception($"Item {item.Sku} with {item.Quantity} items could not be processed.");
                }            
            }

            return output;
        }

        public decimal GetPriceBasedOnPromotions(List<ItemCart> items)
        {
            decimal output = 0M;
            output += items.Count > 1 ? ApplyMultipleProductPromotions(items) : 0M;
            output += ApplySingleProductPromotions(items);
            return output;
        }
    }
}
