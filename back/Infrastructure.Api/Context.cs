using CQRS;
using Microsoft.AspNetCore.Http;

namespace Cine.Together.Api.Infrastructure;

//TODO : sortir dans une lib commune
public class Context(IHttpContextAccessor httpContextAccessor) : IContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string UserId => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "public";
}
