using MarketPlace.Core.Entities.Admin;

namespace MarketPlace.Core.Entities;

public class Product
{
    public int Id { get; set; }
     public string OwnerUserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; }
    public int Quantity { get; set; }
    
    public double Price { get; set; }   
    public List<UserProductCard> UserProductCards { get; set; }
    public List<UserProduct> UsersProducts { get; set; }
    public List<Vaucer> Vaucers { get; set; }

}
