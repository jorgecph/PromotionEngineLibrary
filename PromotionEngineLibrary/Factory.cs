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

        public static Cart CreateCart()
        {
            return new Cart();
        }

        public static IProduct CreateNewProduct(string sku, decimal price)
        {
            return new Product(sku, price);
        }
    }
}
