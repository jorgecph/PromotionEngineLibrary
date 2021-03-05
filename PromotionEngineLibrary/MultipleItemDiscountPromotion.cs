using PromotionEngineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class MultipleItemDiscountPromotion : DiscountPromotion
    {
        private IStore store;

        public MultipleItemDiscountPromotion(IStore store)
        {
            this.store = store;
        }

        public override decimal CalculatePrice(Dictionary<string, int> items)
        {
            if (items.Count < 2)
            {
                return 0M;
            }

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
    }
}
