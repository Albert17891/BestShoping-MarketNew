using MediatR;

namespace MarketPlace.Core.Commands;

public class CreateProductCommand:IRequest<int>
{
    public string OwnerUserId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
