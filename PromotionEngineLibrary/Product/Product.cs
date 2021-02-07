using System;

namespace PromotionEngineLibrary
{
    public class Product : IProduct, IEquatable<Product>
    {
        public string Sku { get; set; }
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

        public override bool Equals(object obj) => Equals(obj as Product);
        public override int GetHashCode() => Sku.GetHashCode();

        public bool Equals(Product other)
        {
            if (other == null)
            {
                return false;
            }

            return Equals(other.Sku, Sku);
        }
    }
}
