namespace MarketPlace.Core.Entities.Discount;

public class DiscountRequestServiceModel
{
    public int ProductId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Percent { get; set; }
}
