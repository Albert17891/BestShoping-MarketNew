using Mapster;
using MarketPlace.Core.Commands;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Repository;
using MediatR;

namespace MarketPlace.Core.Handlers.CommandHandlers.ProductHandlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository<Product>().Update(request.Adapt<Product>());

        await _unitOfWork.SaveChangeAsync();

        return default;
    }
}
