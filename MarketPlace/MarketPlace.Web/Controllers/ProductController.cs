using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Interfaces.Services;
using MarketPlace.Web.ApiModels.Request;
using MarketPlace.Web.ApiModels.Response;
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

    public ProductController(IProductService productService, UserManager<AppUser> userManager)
    {
        _productService = productService;
        _userManager = userManager;
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken = default)
    {
        var products = await _productService.GetProductsAsync(cancellationToken);

        return Ok(products.Adapt<IList<ProductResponse>>());
    }

    [Authorize(Roles = "Manager")]
    [Route("get-my-product")]
    [HttpGet]
    public async Task<IActionResult> GetMyProduct(CancellationToken cancellationToken = default)
    {
        var userName = HttpContext.User.FindFirst("name").Value;
        var user = await _userManager.FindByNameAsync(userName);

        var result = await _productService.GetMyProductsAsync(user.Id, cancellationToken);
        return Ok(result);
    }


    [Authorize(Roles = "Manager")]
    [Route("product-add")]
    [HttpPost]
    public async Task<IActionResult> ProductAdd(ProductRequest productRequest, CancellationToken cancellationToken = default)
    {
        if (productRequest is null)
        {
            return BadRequest();
        }

        var result = await _productService.AddProductAsync(productRequest.Adapt<Product>(), cancellationToken);

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
