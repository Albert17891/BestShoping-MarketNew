using MarketPlace.Core.Entities.Enums;

namespace MarketPlace.Core.Entities;

public class UserAccount
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public double Amount { get; set; } = 10000;//Default
    public string Iban { get; set; }

    public Currency Currency { get; set; } = 0;//Default is Gel

    public AppUser AppUser { get; set; }
}
