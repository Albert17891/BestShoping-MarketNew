using Mapster;
using MarketPlace.Core.Commands;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;
using MarketPlace.Core.Entities.Admin.Response;
using MarketPlace.Core.Exceptions;
using MarketPlace.Core.Interfaces.Repository;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace MarketPlace.Core.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(UserManager<AppUser> userManager, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateVaucerAsync(VaucerServiceModel vaucerServiceModel, CancellationToken cancellationToken)
    {
        var vaucerName = GenerateVaucerName(vaucerServiceModel.UserId);

        vaucerServiceModel.VaucerName = vaucerName;

        await _unitOfWork.Repository<Vaucer>().AddAsync(vaucerServiceModel.Adapt<Vaucer>());

        await _unitOfWork.SaveChangeAsync();
    }

    private string GenerateVaucerName(string Id)
    {
        Random random = new Random();

        var number = random.Next(1000);

        var name = Convert.ToBase64String(Encoding.UTF8.GetBytes(Id + number.ToString()));

        return name;
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>().Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        var query = product.Adapt<DeleteProductCommand>();

        await _mediator.Send(query, cancellationToken);
    }

    public async Task<bool> DeleteUserAsync(AppUser user, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var existUser = await _userManager.FindByEmailAsync(user.Email);

        if (existUser == null)
            throw new UserNotFoundException("User Not Found ");

        var result = await _userManager.DeleteAsync(existUser);

        return result.Succeeded;
    }

    public async Task<IList<ProductWithOwner>> GetProductWithOwnersAsync(CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        var users = await _userManager.Users.ToListAsync();

        var productWithOwners = new List<ProductWithOwner>();

        foreach (var user in users)
        {
            var filterResult = result.Where(x => x.OwnerUserId == user.Id)
                  .Select(x => new ProductWithOwner
                  {
                      ProductId = x.Id,
                      Name = x.Name,
                      Quantity = x.Quantity,
                      Price = x.Price,
                      FirstName = user.FirstName,
                      Email = user.Email
                  }).ToList();

            productWithOwners.AddRange(filterResult);
        }
        return productWithOwners;
    }

    public async Task<bool> UpdateUserAsync(AppUser user, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var existUser = await _userManager.FindByEmailAsync(user.Email);

        if (user == null)
            throw new UserNotFoundException("User Not Found ");

        existUser.FirstName = user.FirstName;
        existUser.LastName = user.LastName;
        existUser.Email = user.Email;

        var result = await _userManager.UpdateAsync(existUser);

        return result.Succeeded;
    }

    public async Task<IList<VaucerAdminResponse>> GetVaucersAsync(CancellationToken cancellationToken)
    {
        var vaucers = await _unitOfWork.Repository<Vaucer>().Table
            .Include(x => x.AppUser)
            .Include(x => x.Product)
            .Select(
               x => new VaucerAdminResponse 
               { Id = x.Id, ExpireTime = x.ExpireTime, UserName = x.AppUser.UserName, ProductName = x.Product.Name,IsUsed=x.IsUsed })
            .ToListAsync(cancellationToken);

        return vaucers;
    }

    public async Task DeleteVaucerAsync(int id, CancellationToken cancellationToken)
    {
        var vaucer = await _unitOfWork.Repository<Vaucer>().Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (vaucer is null)
            throw new NullReferenceException("Vaucer is not Exist");

        _unitOfWork.Repository<Vaucer>().Remove(vaucer);

        await _unitOfWork.SaveChangeAsync();
    }
}
