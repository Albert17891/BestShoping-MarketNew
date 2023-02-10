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

		return Ok();
	}
}
