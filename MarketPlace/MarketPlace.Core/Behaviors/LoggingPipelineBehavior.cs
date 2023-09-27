using MarketPlace.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Core.Behaviors;
public class LoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Product
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest,TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    { 
        _logger.LogInformation($"Starting request {0},{1}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        _logger.LogInformation("Compliting request {@RequestName},{@DateTimeUtc}",
           typeof(TRequest).Name,
           DateTime.UtcNow);

        return result;
    }
}
