using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public struct ItemCart
    {
        public ItemCart(string sku, int quentity)
        {
            Sku = sku;
            Quantity = quentity;
        }

        public string Sku;
        public int Quantity;
    }
}
