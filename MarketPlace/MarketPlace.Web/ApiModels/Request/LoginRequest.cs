using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Request;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
