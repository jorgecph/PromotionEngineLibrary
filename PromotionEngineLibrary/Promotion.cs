using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class Promotion : IPromotion
    {
        public decimal Cost { get; set; }
        public decimal Discount { get; set; } = 0M;
        public List<IProduct> InvolvedProducts { get; set; }
    }
}
