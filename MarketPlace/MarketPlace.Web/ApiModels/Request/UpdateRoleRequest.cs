using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request;

public class UpdateRoleRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
