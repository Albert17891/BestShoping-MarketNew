using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin.Report;
using MarketPlace.Core.Entities.Roles;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services;

public class ReportService : IReportService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ReportService(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
    {
        _unitOfWork = unitOfWork;
        _roleManager = roleManager;
    }
    public async Task<IList<TopProduct>> GetTopTenProductsAsync(CancellationToken cancellationToken)
    {
        var topProducts = await _unitOfWork.Repository<UserProductCard>().Table
                                       .Where(x => x.IsBought == true && x.BoughtTime > DateTime.Today.AddDays(-30))
                                       .GroupBy(x => x.ProductId)
                                       .Select(x => new TopProduct
                                       {
                                           SumQuantity = x.Sum(x => x.Quantity),
                                           Name = x.Select(x => x.Name).First(),
                                           Price = x.Select(x => x.Price).First()
                                       }).OrderByDescending(x => x.SumQuantity).Take(10)
                                       .ToListAsync(cancellationToken);

        return topProducts;
    }

    public async Task<IList<TopSeller>> GetTopTenSellersAsync(CancellationToken cancellationToken)
    {

        var roles = await _roleManager.FindByNameAsync(RolesEnum.Manager.ToString());

        var users = await _unitOfWork.Repository<IdentityUserRole<string>>().Table.Where(x => x.RoleId == roles.Id).ToListAsync();


        var topSellers = new List<TopSeller>();

        foreach (var user in users)
        {
            var topSeller = await _unitOfWork.Repository<AppUser>()
                                          .Table
                                           .Include(x => x.Products)
                                           .Include(x => x.UserAccounts)
                                           .Include(x => x.Vaucers)
                                           .Where(x => x.Id == user.UserId)
                                          .Select(x => new TopSeller
                                          {
                                              Name = x.FirstName,
                                              MoneySum = x.UserAccounts.Select(x => x.Amount).First(),
                                              ProductQantitySum = x.Products.Count(),
                                              ProductAmountSum = x.Products.Select(x => x.Price).Sum()
                                          }).OrderByDescending(x=>x.MoneySum)
                                            .SingleOrDefaultAsync(cancellationToken);

            topSellers.Add(topSeller);
        }

        return topSellers;
    }



    public async Task<IList<TopUser>> GetTopTenUsersAsync(CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<UserProductCard>().Table.Include(x => x.AppUser).Where(x => x.IsBought == true)
                                                                         .GroupBy(x => x.UserId)
                                                                         .Select(x => new TopUser
                                                                         {
                                                                             Name = x.Select(x => x.AppUser.FirstName).First(),
                                                                             MoneySum = x.Select(x => x.SumPrice).Sum()
                                                                         }).OrderByDescending(x => x.MoneySum).Take(10)
                                                                         .ToListAsync(cancellationToken);

        return users;
    }
}
