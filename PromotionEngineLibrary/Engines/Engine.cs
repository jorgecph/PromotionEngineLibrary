using System;
using System.Collections.Generic;

namespace PromotionEngineLibrary
{
    public abstract class Engine
    {
        public abstract decimal CalculatePrice(Cart cart, Store store);
    }
}
