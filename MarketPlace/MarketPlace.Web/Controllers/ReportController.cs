using MarketPlace.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize(Roles ="Admin")]

public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
	{
		_reportService = reportService;
	}

	[Route("get-top-ten")]
	[HttpGet]
	public async Task<IActionResult> GetTopProducts(CancellationToken cancellationToken=default)
	{
		var result = await _reportService.GetTopTenProductsAsync(cancellationToken);

		return Ok(result);
	}

    [Route("get-top-ten-user")]
    [HttpGet]
    public async Task<IActionResult> GetTopUsers(CancellationToken cancellationToken = default)
    {
        var result = await _reportService.GetTopTenUsersAsync(cancellationToken);

        return Ok(result);
    }

    [Route("get-top-ten-seller")]
    [HttpGet]
    public async Task<IActionResult> GetTopSellers(CancellationToken cancellationToken = default)
    {
        var result = await _reportService.GetTopTenSellersAsync(cancellationToken);

        return Ok(result);
    }


}
