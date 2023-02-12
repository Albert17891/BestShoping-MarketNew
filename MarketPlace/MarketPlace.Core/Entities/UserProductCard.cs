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
    public double SumPrice { get; set; }
    public bool IsBought { get; set; } = false;
    public DateTime BoughtTime { get; set; }
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    public AppUser AppUser { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }
}
