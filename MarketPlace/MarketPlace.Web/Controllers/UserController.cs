using Mapster;
using MarketPlace.Core.Entities.Admin;
using MarketPlace.Core.Entities.User;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Web.ApiModels.Request.User;
using MarketPlace.Web.ApiModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize(Roles ="User")]

public class UserController : ControllerBase
{
    private readonly IVaucerService _vaucerService;
    private readonly IUserAccountService _userAccountService;

    public UserController(IVaucerService vaucerService,IUserAccountService userAccountService)
	{
		_vaucerService = vaucerService;
		_userAccountService = userAccountService;
	}

	[Route("get-user-vaucer")]
	[HttpGet]
	public async Task<IActionResult> GetUsersVaucer(string userId,CancellationToken cancellationToken)
	{
		var vaucers = await _vaucerService.GetVaucersByUserIdAsync(userId, cancellationToken);

		return Ok(vaucers.Adapt<IList<VaucerResponse>>());
	}

	[Route("get-user-amount")]
	[HttpGet]
	public async Task<IActionResult> GetUserAmount(string userId,CancellationToken cancellationToken)
	{
		var account = await _userAccountService.GetUserAmountAsync(userId, cancellationToken);

		return Ok(account.Adapt<UserAccountResponse>());
	}

	[Route("buy-product")]
	[HttpPost]
	public async Task<IActionResult> BuyProduct(BuyProductRequest buyProductRequest,CancellationToken cancellationToken)
	{
		if (buyProductRequest is null)
			return BadRequest();

		await _userAccountService.BuyProductAsync(buyProductRequest.Adapt<BuyProductServiceModel>(),cancellationToken);

		return Ok();
	}

	[Route("use-vaucer")]
	[HttpPost]
	public async Task<IActionResult> UseVaucer(UseVaucerRequest vaucerRequest,CancellationToken cancellationToken)
	{
		var result = await _vaucerService.UseVaucerAsync(vaucerRequest.Adapt<VaucerServiceModel>(), cancellationToken);

		return Ok(result);
	}
}
