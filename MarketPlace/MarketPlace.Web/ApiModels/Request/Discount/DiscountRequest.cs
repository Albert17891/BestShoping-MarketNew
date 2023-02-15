namespace MarketPlace.Web.ApiModels.Request.Discount;

public class DiscountRequest
{
    public int ProductId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Percent { get; set; }
}
