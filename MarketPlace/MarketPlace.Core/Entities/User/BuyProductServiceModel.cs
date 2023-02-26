namespace MarketPlace.Core.Entities.User;

public class BuyProductServiceModel
{
    public string UserId { get; set; }   
    public List<BuyProductInfo> BuyProducts { get; set; }
}
