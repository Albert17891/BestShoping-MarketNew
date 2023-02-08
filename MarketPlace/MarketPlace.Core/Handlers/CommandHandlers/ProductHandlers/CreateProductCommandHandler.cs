using Mapster;
using MarketPlace.Core.Commands;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;
using MediatR;

namespace MarketPlace.Core.Handlers.CommandHandlers.ProductHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = request.Adapt<Product>();

        await _unitOfWork.Repository<Product>().AddAsync(product);

        await _unitOfWork.SaveChangeAsync();

        return product.Id;
    }
}