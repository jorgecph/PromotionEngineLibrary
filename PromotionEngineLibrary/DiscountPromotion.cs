using PromotionEngineLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public abstract class DiscountPromotion
    {
        public abstract decimal CalculatePrice(Dictionary<string, int> items);
    }
}
