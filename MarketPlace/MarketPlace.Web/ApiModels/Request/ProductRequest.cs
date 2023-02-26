using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request;

public class ProductRequest
{
    [Required]
    public string OwnerUserId { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(35)]    
    public string Name { get; set; }
    public string Type { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double Price { get; set; }
}
