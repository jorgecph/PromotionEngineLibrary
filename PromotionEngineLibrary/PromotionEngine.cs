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
            foreach(var promotion in _promotions)
            {

            }

            return 0M;
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
