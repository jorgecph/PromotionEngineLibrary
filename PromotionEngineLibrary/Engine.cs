using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            decimal output = 0M;

            output = CalculatePrice(cart, CalculatePromotions);

            if (output != -1)
            {
                return output;
            }

            return cart.Contents.Sum(item => products.Find(product => Equals(product.Sku, item.Sku)).Price * item.Quantity);
        }

        internal decimal CalculatePrice(Cart cart, Func<List<IProduct>, int, decimal> calculatePromotions)
        {
            decimal output = -1;
            decimal promotionValue = 0;
            List<ItemCart> processedItems = new List<ItemCart>();
            processedItems.AddRange(cart.Contents);

            // Simple case, promotion involves one product
            foreach(var item in cart.Contents)
            {
                List<IProduct> productsInCart = new List<IProduct>();
                promotionValue = calculatePromotions(new List<IProduct> { products.Find(product => Equals(product.Sku, item.Sku)) }, item.Quantity);
                if (promotionValue != -1)
                {
                    return promotionValue;
                    //processedItems.Remove(item);
                }
            }

            return output;
        }

        private decimal CalculatePromotions(List<IProduct> products, int quantity)
        {
            foreach(var promotion in currentPromotions)
            {
                if (promotion.InvolvedProducts.Except(products).Count() == 0 && quantity >= promotion.NumberOfProducts)
                {
                    return promotion.Cost;
                }
            }

            return -1;
        }
    }
}
