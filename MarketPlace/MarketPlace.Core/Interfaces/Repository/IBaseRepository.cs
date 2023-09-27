namespace MarketPlace.Core.Interfaces;

public interface IBaseRepository<T>
{
    IQueryable<T> Table { get; }
    Task AddAsync(T entity);
    void Update(T entity);

    void Remove(T entity);
}
