using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class SingleProductDiscountPromotion : DiscountPromotion
    {
        private IStore store;

        public SingleProductDiscountPromotion(IStore store)
        {
            this.store = store;
        }

        public override decimal CalculatePrice(Dictionary<string, int> items)
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
    }
}
