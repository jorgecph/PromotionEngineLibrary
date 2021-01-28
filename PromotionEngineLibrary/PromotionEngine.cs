using System.Collections.Generic;
using System.Linq;

namespace PromotionEngineLibrary
{
    internal class PromotionEngine
    {
        private List<IPromotion> promotions;

        public PromotionEngine(List<IPromotion> incomingPromotions)
        {
            promotions = incomingPromotions;
        }

        private decimal CalculateSimplePromotions(List<IProduct> products, int quantity, out int missingItems)
        {
            decimal output = 0;
            missingItems = quantity;

            foreach (var promotion in promotions)
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

        private decimal ApplyMultipleProductPromotions(Cart cart)
        {
            List<ItemCart> involvedItems = new List<ItemCart>();
            decimal output = 0;

            promotions.ForEach(delegate (IPromotion promotion)
            {
                if (promotion.InvolvedProducts.Count() > 1)
                {
                    promotion.InvolvedProducts.ForEach(delegate (IProduct involvedProduct)
                    {
                        ItemCart foundItem = cart.Contents.Find(i => Equals(i.Sku, involvedProduct.Sku));

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

                    Utilities.ReplaceItems(cart.Contents, involvedItems);
                }
            });

            return output;
        }

        private decimal ApplySingleProductPromotions(List<ItemCart> items, List<IProduct> products)
        {
            decimal output = 0M;
            decimal promotionValue = 0;
            int missingItems;

            foreach (var item in items)
            {
                if (item.Quantity == 0)
                {
                    continue;
                }

                promotionValue = CalculateSimplePromotions(new List<IProduct> { products.Find(product => Equals(product.Sku, item.Sku)) }, item.Quantity, out missingItems);
                output += promotionValue + products.Find(product => Equals(product.Sku, item.Sku)).Price * missingItems;
            }

            return output;
        }

        public decimal GetPriceBasedOnPromotions(Cart cart, List<IProduct> products)
        {
            decimal output = 0M;
            output += cart.Contents.Count > 1 ? ApplyMultipleProductPromotions(cart) : 0M;
            output += ApplySingleProductPromotions(cart.Contents, products);
            return output;
        }
    }
}
