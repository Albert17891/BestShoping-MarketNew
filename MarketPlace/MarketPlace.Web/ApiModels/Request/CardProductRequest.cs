using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request;

public class CardProductRequest
{ 
    [Required]
    public string UserId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public double SumPrice { get; set; }

}
