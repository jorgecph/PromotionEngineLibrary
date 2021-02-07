using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLibrary
{
    public class Store
    {
        public List<IProduct> Products { get; private set; } = new List<IProduct>();
        public List<IPromotion> Promotions { get; private set; } = new List<IPromotion>();

        public void AddProduct(IProduct product)
        {
            Products.Add(product);
        }

        public void AddPromotion(IPromotion promotion)
        {
            Promotions.Add(promotion);
        }

        public void AddPromotion(decimal cost, int numberOfProducts, List<IProduct> involvedProducts)
        {
            AddPromotion(new Promotion() { Cost = cost, NumberOfProducts = numberOfProducts, InvolvedProducts = involvedProducts });
        }

        public void AddPromotion(int discount, int numberOfProducts, List<IProduct> involvedProducts)
        {
            AddPromotion(new Promotion() { Discount = discount, NumberOfProducts = numberOfProducts, InvolvedProducts = involvedProducts });
        }

        public void AddPromotion(decimal cost, List<IProduct> involvedProducts)
        {
            AddPromotion(new Promotion() { Cost = cost, InvolvedProducts = involvedProducts });
        }

        public bool RemovePromotion(IPromotion promotion)
        {
            return Promotions.Remove(promotion);
        }

        public IProduct GetProduct(string sku)
        {
            return Products.Find(p => p.Sku.Equals(sku));
        }
    }
}
