namespace MarketPlace.Web.ApiModels.Request;

public class ProductRequest
{
    public string OwnerUserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
