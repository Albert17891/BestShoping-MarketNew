using MarketPlace.Core.Entities.Admin;
using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Core.Entities;

public class AppUser:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<UserProductCard> UserProductCards { get; set; }

    public List<Vaucer> Vaucers { get; set; }

    public List<UserAccount> UserAccounts { get; set; }

    public List<Product> Products { get; set; }
}
