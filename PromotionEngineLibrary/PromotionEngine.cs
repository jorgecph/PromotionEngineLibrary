﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    internal class PromotionEngine
    {
        private List<IPromotion> _promotions;

        public PromotionEngine(List<IPromotion> promotions)
        {
            _promotions = promotions;
        }

        public decimal ApplyPromotion(ref Cart cart)
        {
            List<ItemCart> involvedItems = new List<ItemCart>();
            decimal output = 0;

            foreach(var promotion in _promotions)
            {
                if (promotion.InvolvedProducts.Count() > 1)
                {
                    foreach(var involvedProduct in promotion.InvolvedProducts)
                    {
                        ItemCart foundItem = cart.Contents.Find(i => Equals(i.Sku, involvedProduct.Sku));
                        if (foundItem.Quantity > 0)
                        {
                            involvedItems.Add(foundItem);
                        }
                    }

                    if (promotion.InvolvedProducts.Count == involvedItems.Count)
                    {
                        while(!involvedItems.Exists(i => i.Quantity == 0))
                        {
                            involvedItems = DecrementQuantity(involvedItems);
                            output += promotion.Cost;
                        }
                    }

                    ReplaceItems(ref cart, involvedItems);
                }
            }

            return output;
        }

        private static void ReplaceItems(ref Cart cart, List<ItemCart> newItems)
        {
            foreach (var newItem in newItems)
            {
                cart.Contents.Remove(cart.Contents.Find(i => Equals(i.Sku, newItem.Sku)));
                cart.Contents.Add(newItem);
            }
        }

        private static List<ItemCart> DecrementQuantity(List<ItemCart> items)
        {
            var output = new List<ItemCart>();

            foreach(var item in items)
            {
                output.Add(new ItemCart { Sku = item.Sku, Quantity = item.Quantity - 1 });
            }

            return output;
        }

        public decimal CalculateSimplePromotions(List<IProduct> products, int quantity, out int missingItems)
        {
            decimal output = 0;
            missingItems = quantity;

            foreach (var promotion in _promotions)
            {
                if (promotion.InvolvedProducts.Except(products).Count() == 0 && promotion.InvolvedProducts.Count() == 1)
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
    }
}
