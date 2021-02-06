using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public class Cart
    {
        public List<ItemCart> Contents { get; private set; } = new List<ItemCart>();

        public void AddItem(string sku, int quantity)
        {
            Contents.Add(new ItemCart(sku, quantity));
        }

        public void AddItem(ItemCart item)
        {
            Contents.Add(item);
        }

        public void ClearItems()
        {
            Contents.Clear();
        }
    }
}
