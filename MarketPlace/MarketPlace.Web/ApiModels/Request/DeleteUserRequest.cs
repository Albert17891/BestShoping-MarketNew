using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request;

public class DeleteUserRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
