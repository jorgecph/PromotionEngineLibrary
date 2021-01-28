using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    internal static class Utilities
    {
        public static List<ItemCart> ReplaceItems(List<ItemCart> cartItems, List<ItemCart> newItems)
        {
            foreach (var newItem in newItems)
            {
                cartItems.Remove(cartItems.Find(i => Equals(i.Sku, newItem.Sku)));
                cartItems.Add(newItem);
            }

            return cartItems;
        }

        public static List<ItemCart> DecrementQuantity(List<ItemCart> items)
        {
            var output = new List<ItemCart>();

            foreach (var item in items)
            {
                output.Add(new ItemCart { Sku = item.Sku, Quantity = item.Quantity - 1 });
            }

            return output;
        }
    }
}
