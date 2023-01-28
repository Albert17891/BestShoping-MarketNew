using Mapster;
using MarketPlace.Core.Entities;
using MarketPlace.Core.Entities.Roles;
using MarketPlace.Core.Interfaces;
using MarketPlace.Web.ApiModels.Request;
using MarketPlace.Web.ApiModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken = default)
    {
        var products = await _productService.GetProductsAsync(cancellationToken);

        return Ok(products.Adapt<IList<ProductResponse>>());
    }

    [Authorize(Roles ="Manager")]
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
}
