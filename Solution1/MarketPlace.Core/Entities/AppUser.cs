namespace MarketPlace.Core.Entities;

public class AppUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<UserProduct> UserProducts { get; set; }
}
