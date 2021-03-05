using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public interface ICart
    {
        Dictionary<string, int> Contents { get; }

        void AddItem(ItemCart item);
        void AddItem(string sku, int quantity);
        decimal CalculatePrice(IStore store);
        void ClearItems();
        ICart Copy();
        void SetPromotionStrategy(Engine engine);
    }
}