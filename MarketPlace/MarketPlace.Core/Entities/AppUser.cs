using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Core.Entities;

public class AppUser:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<UserProduct> UserProducts { get; set; }

    public List<UserProductCard> UserProductCards { get; set; }
}
