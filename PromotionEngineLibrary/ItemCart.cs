using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    internal struct ItemCart
    {
        public ItemCart(IProduct product, int quentity)
        {
            Product = product;
            Quantity = quentity;
        }

        IProduct Product;
        int Quantity;
    }
}
