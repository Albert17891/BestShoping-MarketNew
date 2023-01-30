using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Account;
using MarketPlace.Web.ApiModels.Request;
using MarketPlace.Web.ApiModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]

[Authorize(Roles ="Admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserAuthentication _userAuthentication;   

    public AdminController(UserManager<AppUser> userManager,IUserAuthentication userAuthentication)
	{
		_userManager = userManager;
		_userAuthentication = userAuthentication;
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

		var result=await _userManager.RemoveFromRoleAsync(user, "User");

		if (result.Succeeded)
		{
            await _userManager.AddToRoleAsync(user, "Manager");
			return Ok();
        }		

		return BadRequest();
	}
}
