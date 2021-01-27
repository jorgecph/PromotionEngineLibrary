using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class Engine
    {
        public List<IPromotion> CurrentPromotions { set; get; } = new List<IPromotion>();

        public decimal CalculatePrice(Cart cart)
        {
            decimal output = 0M;

            output = CalculatePrice(cart, CalculatePromotions);

            if (output != -1)
            {
                return output;
            }

            return cart.Contents.Sum(p => p.Product.Price * p.Quantity);
        }

        internal decimal CalculatePrice(Cart cart, Func<List<Product>, decimal> calculatePromotions)
        {
            decimal output = 0M;
            decimal promotionValue = 0;
            List<ItemCart> processedItems = cart.Contents;

            // Simple case, promotion involves one product
            foreach(var item in cart.Contents)
            {
                promotionValue = calculatePromotions(new List<Product> { item.Product });
                if (promotionValue != -1)
                {
                    output += promotionValue;
                    processedItems.Remove(item);
                }
            }

            return output;
        }

        private decimal CalculatePromotions(List<Product> products)
        {
            foreach(var promotion in CurrentPromotions)
            {
                if (promotion.InvolvedProducts.Except(products).Count() == 0)
                {
                    return promotion.Cost;
                }
            }

            return -1;
        }
    }
}
