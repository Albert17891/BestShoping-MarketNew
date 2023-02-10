namespace MarketPlace.Core.Entities.Admin.Response;

public class VaucerResponse
{
    public int Id { get; set; }
    public DateTime ExpireTime { get; set; }
    public string UserName { get; set; }
    public string ProductName { get; set; }
}
