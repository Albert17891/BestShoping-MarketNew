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

    public AdminController(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	[HttpGet]
	public async Task<IActionResult> GetUsers()
	{
		var users = _userManager.Users.ToListAsync();

		if (users == null)
			return NotFound();

		return Ok(users);
	}

	[HttpPost]
	public async Task<IActionResult> UpdateRole()
	{
		//Todo
		return NotFound();
	}
}
