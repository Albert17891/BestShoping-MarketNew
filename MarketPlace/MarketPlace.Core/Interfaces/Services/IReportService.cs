using MarketPlace.Core.Entities.Admin.Report;

namespace MarketPlace.Core.Interfaces.Services;

public interface IReportService
{
    Task<IList<TopProduct>> GetTopTenProductsAsync(CancellationToken cancellationToken);
}
