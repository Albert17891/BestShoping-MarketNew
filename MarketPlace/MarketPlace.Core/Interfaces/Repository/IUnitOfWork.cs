namespace MarketPlace.Core.Interfaces.Repository;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    Task<int> SaveChangeAsync();
}
