using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MarketPlace.Core.Entities.Admin;
public class Vaucer
{
    public int Id { get; set; }
    public string VaucerName { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime ExpireTime { get; set; }
    public double Price { get; set; }
    public bool IsBlocked { get; set; } = false;
    public bool IsUsed { get; set; } = false;

    public AppUser AppUser { get; set; }
    public Product Product { get; set; }
}
