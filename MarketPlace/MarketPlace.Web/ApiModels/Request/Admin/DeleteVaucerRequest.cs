using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request.Admin;

public class DeleteVaucerRequest
{
    [Required]
    public int Id { get; set; }
}
