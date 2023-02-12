namespace MarketPlace.Core.Entities.Admin;

public class VaucerServiceModel
{
    public int Id { get; set; }
    public string VaucerName { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime ExpireTime { get; set; }
    public double Price { get; set; }
}
