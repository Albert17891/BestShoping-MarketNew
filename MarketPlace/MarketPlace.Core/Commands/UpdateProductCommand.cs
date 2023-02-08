using MediatR;

namespace MarketPlace.Core.Commands;

public class UpdateProductCommand:IRequest
{
    public int Id { get; set; }
    public string OwnerUserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
