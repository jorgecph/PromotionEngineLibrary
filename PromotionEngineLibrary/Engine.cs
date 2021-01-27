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
            decimal promotionValue = 0;
            int missingItems;
            List<ItemCart> processedItems = new List<ItemCart>();
            processedItems.AddRange(cart.Contents);

            // Simple case, promotion involves one product
            foreach (var item in cart.Contents)
            {
                List<IProduct> productsInCart = new List<IProduct>();
                promotionValue = CalculatePromotions(new List<IProduct> { products.Find(product => Equals(product.Sku, item.Sku)) }, item.Quantity, out missingItems);

                output += promotionValue + products.Find(product => Equals(product.Sku, item.Sku)).Price * missingItems;
            }

            return output;
        }

        private decimal CalculatePromotions(List<IProduct> products, int quantity, out int missingItems)
        {
            decimal output = 0;
            missingItems = quantity;

            foreach(var promotion in currentPromotions)
            {
                if (promotion.InvolvedProducts.Except(products).Count() == 0 && promotion.NumberOfProducts != 0)
                {
                    while (missingItems >= promotion.NumberOfProducts)
                    {
                        missingItems -= promotion.NumberOfProducts;
                        output += promotion.Cost;
                    }
                }
            }

            return output;
        }
    }
}
