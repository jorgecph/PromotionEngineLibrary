using System;
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

        public decimal ApplyPromotion(List<ItemCart> cartItems)
        {
            List<ItemCart> involvedItems = new List<ItemCart>();
            decimal output = 0;

            foreach(var promotion in _promotions)
            {
                if (promotion.InvolvedProducts.Count() > 1)
                {
                    foreach(var involvedProduct in promotion.InvolvedProducts)
                    {
                        ItemCart foundItem = cartItems.Find(i => Equals(i.Sku, involvedProduct.Sku));
                        if (foundItem.Quantity > 0)
                        {
                            involvedItems.Add(foundItem);
                        }
                    }

                    if (promotion.InvolvedProducts.Count == involvedItems.Count)
                    {
                        while(CountMinItems(involvedItems) > 0)
                        {
                            involvedItems = DecrementQuantity(involvedItems);
                            output += promotion.Cost;
                        }
                    }
                }
            }

            return output;
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

        private static int CountMinItems(IList<ItemCart> items)
        {
            int output = -1;

            foreach(var item in items)
            {
                if (item.Quantity == 0)
                {
                    return 0;
                }
                else if (output == -1)
                {
                    output = item.Quantity;
                }
                else if (output < item.Quantity)
                {
                    output = item.Quantity;
                }
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
