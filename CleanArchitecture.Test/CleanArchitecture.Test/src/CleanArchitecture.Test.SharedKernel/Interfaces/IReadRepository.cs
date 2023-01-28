using Ardalis.Specification;

namespace CleanArchitecture.Test.SharedKernel.Interfaces;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
