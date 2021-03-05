using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public static class Factory
    {
        public static PromotionEngine CreatePromotionEngine()
        {
            return new PromotionEngine();
        }

        public static Store CreateStore()
        {
            return new Store();
        }

        public static ICart CreateCart()
        {
            return new Cart();
        }

        public static IProduct CreateProduct(string sku, decimal price)
        {
            return new Product(sku, price);
        }

        public static IPromotion CreatePromotion()
        {
            return new Promotion();
        }
    }
}
