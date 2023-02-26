namespace MarketPlace.Core.Entities.Discount;

public class DiscountResponseServiceModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Percent { get; set; }
    public DateTime EndTime { get; set; }
}
