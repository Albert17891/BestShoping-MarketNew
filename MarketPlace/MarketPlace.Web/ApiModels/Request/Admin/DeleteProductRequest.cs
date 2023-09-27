using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request.Admin;

public class DeleteProductRequest
{
    [Required]
    public int Id { get; set; }
}
