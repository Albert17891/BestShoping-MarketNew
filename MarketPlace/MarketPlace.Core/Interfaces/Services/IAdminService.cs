﻿using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Admin;
using MarketPlace.Core.Entities.Admin.Request;
using MarketPlace.Core.Entities.Admin.Response;

namespace MarketPlace.Core.Interfaces.Services;
public interface IAdminService
{
    Task<bool> UpdateUserAsync(AppUser user, CancellationToken token);
    Task<bool> DeleteUserAsync(AppUser user, CancellationToken token);
    Task<IList<ProductWithOwner>> GetProductWithOwnersAsync(CancellationToken cancellationToken);
    Task DeleteProductAsync(int id, CancellationToken cancellationToken);

    Task CreateVaucerAsync(VaucerServiceModel vaucerServiceModel,CancellationToken cancellationToken);

    Task<IList<VaucerAdminResponse>> GetVaucersAsync(CancellationToken cancellationToken);

    Task DeleteVaucerAsync(int id, CancellationToken cancellationToken);

    Task UpdateRoleAsync(UpdateRoleServiceRequest updateRoleModel,CancellationToken cancellationToken);
}
