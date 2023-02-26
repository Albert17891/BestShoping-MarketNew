using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Response;

public class ProductResponse
{
    public int Id { get; set; }
    public string OwnerUserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }    
    public int Counter { get; set; }
}
