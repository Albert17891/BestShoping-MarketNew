using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Account;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Web.ApiModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]

[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserAuthentication _userAuthentication;
    private readonly IAdminService _adminService;

    public AdminController(UserManager<AppUser> userManager, IUserAuthentication userAuthentication, IAdminService adminService)
    {
        _userManager = userManager;
        _userAuthentication = userAuthentication;
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userAuthentication.GetUsers();

        if (users == null)
            return NotFound();

        return Ok(users);
    }

    [Route("update-role")]
    [HttpPost]
    public async Task<IActionResult> UpdateRole(UpdateRoleRequest roleRequest)
    {
        var user = await _userManager.FindByEmailAsync(roleRequest.Email);

        if (user == null)
            return BadRequest();

        var result = await _userManager.RemoveFromRoleAsync(user, "User");

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Manager");
            return Ok();
        }

        return BadRequest();
    }

    [Route("update-user")]
    [HttpPost]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest updateUser, CancellationToken cancellationToken = default)
    {
        var result = await _adminService.UpdateUserAsync(updateUser.Adapt<AppUser>(), cancellationToken);

        return Ok(result);
    }

    [Route("delete-user")]
    [HttpPost]
    public async Task<IActionResult> DeleteUser(DeleteUserRequest deleteUser, CancellationToken cancellationToken = default)
    {
        var result = await _adminService.DeleteUserAsync(deleteUser.Adapt<AppUser>(), cancellationToken);

        return Ok(result);
    }

    [Route("get-product-with-owner")]
    [HttpGet]
    public async Task<IActionResult> GetProductWithOwner(CancellationToken cancellationToken=default)
    {
        var result = await _adminService.GetProductWithOwnersAsync(cancellationToken);

        return Ok(result);
    }
}
