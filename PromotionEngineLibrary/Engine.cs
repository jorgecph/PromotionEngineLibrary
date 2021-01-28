using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public class Engine
    {
        private List<IPromotion> promotions = new List<IPromotion>();
        private List<IProduct> products = new List<IProduct>();

        public void AddProduct(IProduct product)
        {
            products.Add(product);
        }

        public void AddPromotion(IPromotion promotion)
        {
            promotions.Add(promotion);
        }

        public bool RemovePromotion(IPromotion promotion)
        {
            return promotions.Remove(promotion);
        }

        public decimal CalculatePrice(Cart cart)
        {
            PromotionEngine promotionEngine = new PromotionEngine(promotions, products);

            return promotionEngine.GetPriceBasedOnPromotions(cart.Contents);
        }
    }
}
