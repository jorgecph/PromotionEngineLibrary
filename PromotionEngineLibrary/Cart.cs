using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class Cart
    {
        List<ItemCart> items = new List<ItemCart>();

        public void AddProduct(Product product, int quantity)
        {
            items.Add(new ItemCart(product, quantity));
        }
    }
}
