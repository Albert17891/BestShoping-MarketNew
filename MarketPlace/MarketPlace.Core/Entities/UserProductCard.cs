namespace MarketPlace.Core.Entities;

public class UserProductCard
{
    public int Id { get; set; }
     public string UserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }   
    public int Quantity { get; set; }
    public double Price { get; set; }

    public AppUser AppUser { get; set; }
}
