namespace MarketPlace.Core.Entities;

public class UserProduct
{
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public AppUser AppUser { get; set; }
}
