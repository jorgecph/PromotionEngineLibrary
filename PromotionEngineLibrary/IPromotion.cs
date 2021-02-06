using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public interface IPromotion
    {
        decimal Cost { get; set; }
        int NumberOfProducts { get; set; }
        decimal Discount { get; set; }
        List<IProduct> InvolvedProducts { get; set; }
    }
}