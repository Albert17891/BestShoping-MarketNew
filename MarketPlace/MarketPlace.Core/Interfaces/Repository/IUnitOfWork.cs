namespace MarketPlace.Core.Interfaces.Repository;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICardRepository CardRepository { get; }    
    Task<int> SaveChangeAsync();
}
