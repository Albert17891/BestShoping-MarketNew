using MarketPlace.Core.Entities.Admin;
using System.Text.Json.Serialization;

namespace MarketPlace.Core.Entities;

public class Product
{
    public int Id { get; set; }
     public string OwnerUserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool IsActive { get; set; } = true;
    public int Quantity { get; set; }
    public DateTime DiscountTimeStart { get; set; }
    public DateTime DiscountTimeEnd { get; set; }

    public bool IsDiscount { get; set; } = false;
    public bool IsDiscountActive { get; set; } = false;
    public int DiscountPercent { get; set; }
    public double Price { get; set; }
    [JsonIgnore]
    public AppUser AppUser { get; set; }
    [JsonIgnore]
    public IList<UserProductCard> UserProductCards { get; set; }
    [JsonIgnore]
    public Vaucer Vaucer { get; set; }

}
