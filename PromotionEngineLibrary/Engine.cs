using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public class Engine
    {
        private List<IPromotion> currentPromotions = new List<IPromotion>();
        private List<IProduct> products = new List<IProduct>();

        public void AddProduct(IProduct product)
        {
            products.Add(product);
        }

        public void AddPromotion(IPromotion promotion)
        {
            currentPromotions.Add(promotion);
        }

        public bool RemovePromotion(IPromotion promotion)
        {
            return currentPromotions.Remove(promotion);
        }

        public decimal CalculatePrice(Cart cart)
        {
            PromotionEngine promotionEngine = new PromotionEngine(currentPromotions);

            return promotionEngine.GetPriceBasedOnPromotions(cart, products);
        }
    }
}
