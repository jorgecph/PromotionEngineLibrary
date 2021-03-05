using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public interface IStore
    {
        List<IPromotion> MultipleProductPromotions { get; }
        List<IProduct> Products { get; }
        List<IPromotion> SingleProductPromotions { get; }
        List<DiscountPromotion> DiscountPromotions { get; }

        void AddProduct(IProduct product);
        void AddPromotion(decimal cost, int numberOfProducts, List<IProduct> involvedProducts);
        void AddPromotion(decimal cost, List<IProduct> involvedProducts);
        void AddPromotion(int discount, int numberOfProducts, List<IProduct> involvedProducts);
        void AddPromotion(IPromotion promotion);
        IProduct GetProduct(string sku);
        bool RemovePromotion(IPromotion promotion);
    }
}