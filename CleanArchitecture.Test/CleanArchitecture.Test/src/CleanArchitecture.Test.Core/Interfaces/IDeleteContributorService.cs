using Ardalis.Result;

namespace CleanArchitecture.Test.Core.Interfaces;
public interface IDeleteContributorService
{
  public Task<Result> DeleteContributor(int contributorId);
}
