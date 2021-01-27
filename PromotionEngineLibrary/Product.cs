using System;

namespace PromotionEngineLibrary
{
    public class Product : IProduct
    {
        public string Sku { get; init; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Product(string sku, string description, decimal price)
        {
            Sku = sku;
            Description = description;
            Price = price;
        }

        public Product(string sku, decimal price)
        {
            Sku = sku;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            Product other = obj as Product;

            if (other != null)
            {
                return Equals(other.Sku, Sku);
            }

            return false;
        }
    }
}
