namespace PromotionEngineLibrary
{
    public struct ItemCart
    {
        public ItemCart(string sku, int quentity)
        {
            Sku = sku;
            Quantity = quentity;
        }

        public string Sku;
        public int Quantity;
    }
}
