using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public class Promotion : IPromotion
    {
        public decimal Cost { get; set; }
        public int NumberOfProducts { get; set; }
        public decimal Discount { get; set; } = 0M;
        public List<IProduct> InvolvedProducts { get; set; }
    }
}
