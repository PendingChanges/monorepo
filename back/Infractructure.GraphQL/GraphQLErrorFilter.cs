using Microsoft.Extensions.Logging;

namespace Infractructure.GraphQL;

public class GraphQLErrorFilter(ILogger<GraphQLErrorFilter> logger) : IErrorFilter
{
    private readonly ILogger<GraphQLErrorFilter> _logger = logger;

    public IError OnError(IError error)
    {
        _logger.LogError(error.Exception, error.Exception?.Message ?? error.Message);

        return error;
    }
}
