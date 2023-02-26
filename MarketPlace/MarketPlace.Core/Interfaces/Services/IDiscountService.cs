using MarketPlace.Core.Commands;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Discount;

namespace MarketPlace.Core.Interfaces.Services;
public interface IDiscountService
{
    Task<IList<DiscountCheckResponse>> CheckDiscountAsync(CancellationToken cancellationToken);
    Task<IList<DiscountResponseServiceModel>> GetDiscountsAsync(CancellationToken token);
    Task CreateDiscountAsync(DiscountRequestServiceModel discountRequest, CancellationToken token);

    Task DeleteDiscountAsync(int id,CancellationToken token);
}
