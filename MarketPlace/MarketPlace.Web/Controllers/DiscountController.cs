using Mapster;
using MarketPlace.Core.Entities.Discount;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Web.ApiModels.Request.Discount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]

public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
	{
		_discountService = discountService;
	}

	[Authorize(Roles ="Manager,User")]
	[Route("get-discounts")]
	[HttpGet]
	public async Task<IActionResult> GetDiscounts(CancellationToken token=default)
	{
		var discounts = await _discountService.GetDiscountsAsync(token);

		return Ok(discounts);
	}

	[Authorize(Roles ="Manager")]
	[Route("create-discount")]
	[HttpPost]
	public async Task<IActionResult> CreateDiscount(DiscountRequest discountRequest,CancellationToken token=default)
	{
		if (discountRequest == null)
			return BadRequest();

	  	await _discountService.CreateDiscountAsync(discountRequest.Adapt<DiscountRequestServiceModel>(), token);

		return Ok();
	}

    [Authorize(Roles = "Manager")]
    [Route("delete-discount")]
    [HttpGet]
    public async Task<IActionResult> DeleteDiscount(int id, CancellationToken token = default)
    {
		await _discountService.DeleteDiscountAsync(id, token);

        return Ok();
    }

    [AllowAnonymous]
	[Route("check-discounts")]
	[HttpGet]
	public async Task<IActionResult> CheckDiscount(CancellationToken token=default)
	{
		var results=await _discountService.CheckDiscountAsync(token);

		return Ok(results);
	}
}
