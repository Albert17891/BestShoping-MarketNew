using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Web.ApiModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]

[Authorize(Roles = "User")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly UserManager<AppUser> _userManager;

    public CardController(ICardService cardService, UserManager<AppUser> userManager)
    {
        _cardService = cardService;
        _userManager = userManager;
    }

    [Route("get-card-products")]
    [HttpGet]
    public async Task<IActionResult> GetCardProducts(string userId,CancellationToken cancellationToken = default)
    {     

        var cardProduct = await _cardService.GetCardProductsAsync(userId, cancellationToken);

        return Ok(cardProduct);
    }

    [Route("add-card-products")]
    [HttpPost]
    public async Task<IActionResult> AddCardProducts(CardProductRequest cardProduct, CancellationToken cancellationToken = default)
    {
        if (cardProduct is null)
            return BadRequest();

        await _cardService.AddCardProductsAsync(cardProduct.Adapt<UserProductCard>(), cancellationToken);

        return Ok();
    }

    [Route("inc-update-card-products")]
    [HttpPost]
    public async Task<IActionResult> UpdateCardProductsIncrement(CardProductUpdateRequest cardProduct, CancellationToken cancellationToken = default)
    {
        if (cardProduct is null)
            return BadRequest();

        await _cardService.UpdateCardProductsIncrementAsync(cardProduct.Adapt<UserProductCard>(), cancellationToken);

        return Ok();
    }

    [Route("dec-update-card-products")]
    [HttpPost]
    public async Task<IActionResult> UpdateCardProductsDecrement(CardProductUpdateRequest cardProduct, CancellationToken cancellationToken = default)
    {
        if (cardProduct is null)
            return BadRequest();

        await _cardService.UpdateCardProductsDecrementAsync(cardProduct.Adapt<UserProductCard>(), cancellationToken);

        return Ok();
    }
}
