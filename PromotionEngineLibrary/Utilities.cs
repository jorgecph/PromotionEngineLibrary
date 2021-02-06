using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    internal static class Utilities
    {
        public static void UpdateQuantities(Dictionary<string, int> cartItems, Dictionary<string, int> newItems)
        {
            foreach (var newItem in newItems)
            {
                cartItems[newItem.Key] = newItem.Value;
            }
        }

        public static void DecrementQuantity(Dictionary<string, int> items, int decrementBy)
        {
            foreach (var item in items)
            {
                items[item.Key] -= decrementBy;
            }
        }
    }
}
