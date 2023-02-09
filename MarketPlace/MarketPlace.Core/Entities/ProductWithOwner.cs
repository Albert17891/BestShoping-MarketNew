namespace MarketPlace.Core.Entities;
public class ProductWithOwner
{
    public string Name { get; set; }   
    public int Quantity { get; set; }
    public double Price { get; set; }

    //Owner FirsName
    public string FirstName { get; set; }
    public string Email { get; set; }
}
