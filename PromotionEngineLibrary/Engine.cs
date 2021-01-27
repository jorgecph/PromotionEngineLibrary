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

            return cart.Contents.Sum(p => p.Product.Price * p.Quantity);
        }
    }
}
