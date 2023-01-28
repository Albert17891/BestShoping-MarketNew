using CleanArchitecture.Test.Core.ContributorAggregate;
using CleanArchitecture.Test.Core.ContributorAggregate.Specifications;
using CleanArchitecture.Test.SharedKernel.Interfaces;
using FastEndpoints;

namespace CleanArchitecture.Test.Web.Endpoints.ContributorEndpoints;
public class GetById : Endpoint<GetContributorByIdRequest, ContributorRecord>
{
  private readonly IRepository<Contributor> _repository;

  public GetById(IRepository<Contributor> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(GetContributorByIdRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ContributorEndpoints"));
  }
  public override async Task HandleAsync(GetContributorByIdRequest request,
    CancellationToken cancellationToken)
  {
    var spec = new ContributorByIdSpec(request.ContributorId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
    if (entity == null)
    {
      await SendNotFoundAsync();
      return;
    }

    var response = new ContributorRecord(entity.Id, entity.Name);

    await SendAsync(response);
  }
}
