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

        public void AddItem(string sku, int quantity)
        {
            Contents.Add(new ItemCart(sku, quantity));
        }

        public void ClearItems()
        {
            Contents.Clear();
        }
    }
}
