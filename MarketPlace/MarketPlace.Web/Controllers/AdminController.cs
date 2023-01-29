using MarketPlace.Core.Entities;
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
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}

	[HttpGet]
	public async Task<IActionResult> GetUsers()
	{
		var users =await _userManager.Users.ToListAsync();

		if (users == null)
			return NotFound();

		return Ok(users);
	}

	
	[HttpPost]
	public async Task<IActionResult> UpdateRole(string userId)
	{
		var user = await _userManager.FindByIdAsync(userId);

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
