namespace MarketPlace.Web.ApiModels.Request.Admin;

public class VaucerRequest
{
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime ExpireTime { get; set; }
    public double Price { get; set; }
}
