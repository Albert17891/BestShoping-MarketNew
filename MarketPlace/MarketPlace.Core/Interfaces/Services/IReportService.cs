using MarketPlace.Core.Entities.Admin.Report;

namespace MarketPlace.Core.Interfaces.Services;

public interface IReportService
{
    Task<IList<TopProduct>> GetTopTenProductsAsync(CancellationToken cancellationToken);
    Task<IList<TopUser>> GetTopTenUsersAsync(CancellationToken cancellationToken);

    Task<IList<TopSeller>> GetTopTenSellersAsync(CancellationToken cancellationToken);
}
