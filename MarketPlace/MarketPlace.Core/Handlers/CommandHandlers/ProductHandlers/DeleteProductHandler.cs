using Mapster;
using MarketPlace.Core.Commands;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;
using MediatR;

namespace MarketPlace.Core.Handlers.CommandHandlers.ProductHandlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository<Product>().Remove(request.Adapt<Product>());

        await _unitOfWork.SaveChangeAsync();

        return default;
    }
}
