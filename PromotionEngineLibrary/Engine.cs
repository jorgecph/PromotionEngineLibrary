using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class Engine
    {
        public static decimal CalculatePrice(Cart cart)
        {
            decimal output = 0M;

            return cart.Contents.Sum(p => p.Product.Price * p.Quantity);
        }

        public void AddPromotions()
        {

        }
    }
}
