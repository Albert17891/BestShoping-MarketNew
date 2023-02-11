namespace MarketPlace.Web.ApiModels.Request.User;

public class BuyProductRequest
{
    public string UserId { get; set; }
   
    public List<BuyProductInfoRequest> BuyProducts { get; set; }
}
