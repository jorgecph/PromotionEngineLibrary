using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngineLibrary
{
    public class PromotionEngine : Engine
    {
        private IStore store;

        public override decimal CalculatePrice(ICart cart, IStore store)
        {
            ICart internalCart = cart.Copy();

            this.store = store;

            return GetPriceBasedOnPromotions(internalCart.Contents);
        }

        private decimal GetPriceBasedOnPromotions(Dictionary<string, int> items)
        {
            decimal output = 0M;

            foreach (var promotion in store.DiscountPromotions)
            {
                output += promotion.CalculatePrice(items);
            }

            return output;
        }
    }
}
