using System.Text.Json.Serialization;

namespace MarketPlace.Core.Entities;

public class UserProductCard
{
    public int Id { get; set; }
    public int ProductId { get; set; }
     public string UserId { get; set; }      
    public string Name { get; set; }
    public string Type { get; set; }   
    public int Quantity { get; set; }
    public double Price { get; set; }

    [JsonIgnore]
    public AppUser AppUser { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }
}
