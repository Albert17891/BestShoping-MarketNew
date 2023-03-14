namespace MarketPlace.Web.Infastructure.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LoggingMiddleware(RequestDelegate next,ILogger logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{

		_logger.LogInformation("Start Log");
		await _next(context);

		_logger.LogInformation("End Log");
	}
}
