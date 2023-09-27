using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request;

public class CardProductUpdateRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Type { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public double SumPrice { get; set; }

}
