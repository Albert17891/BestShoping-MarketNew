using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Web.ApiModels.Response;

public class UserAccountResponse
{
    [Required]
    public double Amount { get; set; }
}
