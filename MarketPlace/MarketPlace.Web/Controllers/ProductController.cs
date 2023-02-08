using Mapster;
using MarketPlace.Core.Commands;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Core.Queries;
using MarketPlace.Web.ApiModels.Request;
using MarketPlace.Web.ApiModels.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]


public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;

    public ProductController(IProductService productService, UserManager<AppUser> userManager,IMediator mediator)
    {

        _productService = productService;
        _userManager = userManager;
        _mediator = mediator;
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken = default)
    {
        var query = new GetProductsQuery();

        var products = await _mediator.Send(query, cancellationToken);

        return Ok(products.Adapt<IList<ProductResponse>>());
    }

    [Authorize(Roles = "Manager")]
    [Route("get-my-product")]
    [HttpGet]
    public async Task<IActionResult> GetMyProduct(CancellationToken cancellationToken = default)
    {
        var userName = HttpContext.User.FindFirst("name").Value;
        var user = await _userManager.FindByNameAsync(userName);

        var query = new GetMyProductQuery(user.Id);

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }


    [Authorize(Roles = "Manager")]
    [Route("product-add")]
    [HttpPost]
    public async Task<IActionResult> ProductAdd(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        if (command is null)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [Authorize(Roles = "Manager")]
    [Route("product-update")]
    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductUpdateRequest productRequest, CancellationToken cancellationToken = default)
    {
        if (productRequest is null)
        {
            return BadRequest();
        }

        await _productService.UpdateProductAsync(productRequest.Adapt<Product>(), cancellationToken);

        return Ok();
    }

    [Authorize(Roles = "Manager")]
    [Route("product-delete")]
    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductUpdateRequest productRequest, CancellationToken cancellationToken = default)
    {
        if (productRequest is null)
        {
            return BadRequest();
        }

        await _productService.DeleteProductAsync(productRequest.Adapt<Product>(), cancellationToken);

        return Ok();
    }


}
