namespace MarketPlace.Core.Interfaces.Repository;

public interface IUnitOfWork
{ 

    IBaseRepository<T> Repository<T>() where T:class;
    Task<int> SaveChangeAsync();
}
