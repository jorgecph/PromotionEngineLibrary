using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public struct ItemCart
    {
        public ItemCart(Product product, int quentity)
        {
            Product = product;
            Quantity = quentity;
        }

        public Product Product;
        public int Quantity;
    }
}
