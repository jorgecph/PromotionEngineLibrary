using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class Cart
    {
        public List<ItemCart> Contents { get; private set; } = new List<ItemCart>();

        public void AddProduct(Product product, int quantity)
        {
            Contents.Add(new ItemCart(product, quantity));
        }
    }
}
