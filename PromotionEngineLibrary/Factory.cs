using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public static class Factory
    {
        public static Engine GetNewEngine()
        {
            return new Engine();
        }

        public static Cart GetNewCart()
        {
            return new Cart();
        }

        public static IProduct GetNewProduct(string sku, decimal price)
        {
            return new Product(sku, price);
        }
    }
}
