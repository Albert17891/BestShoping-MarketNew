using Ardalis.Result;
using CleanArchitecture.Test.Core.ContributorAggregate;
using CleanArchitecture.Test.Core.ContributorAggregate.Events;
using CleanArchitecture.Test.Core.Interfaces;
using CleanArchitecture.Test.SharedKernel.Interfaces;
using MediatR;

namespace CleanArchitecture.Test.Core.Services;
public class DeleteContributorService : IDeleteContributorService
{
  private readonly IRepository<Contributor> _repository;
  private readonly IMediator _mediator;

  public DeleteContributorService(IRepository<Contributor> repository, IMediator mediator)
  {
    _repository = repository;
    _mediator = mediator;
  }

  public async Task<Result> DeleteContributor(int contributorId)
  {
    var aggregateToDelete = await _repository.GetByIdAsync(contributorId);
    if (aggregateToDelete == null) return Result.NotFound();

    await _repository.DeleteAsync(aggregateToDelete);
    var domainEvent = new ContributorDeletedEvent(contributorId);
    await _mediator.Publish(domainEvent);
    return Result.Success();
  }
}
