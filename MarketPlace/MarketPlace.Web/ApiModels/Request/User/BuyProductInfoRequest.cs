namespace MarketPlace.Web.ApiModels.Request.User;

public class BuyProductInfoRequest
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public double Price { get; set; }
}
