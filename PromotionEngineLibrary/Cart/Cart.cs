using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public class Cart
    {
        public Dictionary<string, int> Contents { get; private set; } = new Dictionary<string, int>();
        private Engine engine;

        public void AddItem(string sku, int quantity)
        {
            Contents.Add(sku, quantity);
        }

        public void AddItem(ItemCart item)
        {
            Contents.Add(item.Sku, item.Quantity);
        }

        public void ClearItems()
        {
            Contents.Clear();
        }

        public Cart Copy()
        {
            Cart output = new Cart();

            foreach(var content in Contents)
            {
                output.AddItem(content.Key, content.Value);
            }

            return output;
        }
        public void SetPromotionStrategy(Engine engine)
        {
            this.engine = engine;
        }

        public decimal CalculatePrice(Store store)
        {
            return engine.CalculatePrice(this, store);
        }
    }
}
