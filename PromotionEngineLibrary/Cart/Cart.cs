using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public class Cart : ICart
    {
        public Dictionary<string, int> Contents { get; private set; } = new Dictionary<string, int>();
        private Engine engine;

        public void AddItem(string sku, int quantity)
        {
            Contents.Add(sku, quantity);
        }

        public void AddItem(ItemCart item)
        {
            AddItem(item.Sku, item.Quantity);
        }

        public void ClearItems()
        {
            Contents.Clear();
        }

        public ICart Copy()
        {
            ICart output = Factory.CreateCart();

            foreach (var content in Contents)
            {
                output.AddItem(content.Key, content.Value);
            }

            return output;
        }
        public void SetPromotionStrategy(Engine engine)
        {
            this.engine = engine;
        }

        public decimal CalculatePrice(IStore store)
        {
            return engine.CalculatePrice(this, store);
        }
    }
}
