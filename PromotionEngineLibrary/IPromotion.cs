using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public interface IPromotion
    {
        decimal Cost { get; set; }
        public int NumberOfProducts { get; set; }
        decimal Discount { get; set; }
        List<IProduct> InvolvedProducts { get; set; }
    }
}