namespace PromotionEngineLibrary
{
    public interface IProduct
    {
        string Description { get; set; }
        decimal Price { get; set; }
        string Sku { get; init; }
    }
}