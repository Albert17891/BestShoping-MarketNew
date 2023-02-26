using MarketPlace.Core.Interfaces;
using MarketPlace.Core.Interfaces.Repository;

namespace MarketPlace.Infastructure.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IBaseRepository<T> Repository<T>() where T : class
    {
        if (_repositories is null)
        {
            _repositories = new Dictionary<Type, object>();
        }

        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(BaseRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(type), _context);
            _repositories[type] = repositoryInstance;
        }

        return (IBaseRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

