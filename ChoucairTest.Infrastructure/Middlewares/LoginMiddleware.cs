using ChoucairTest.Domain.Ports;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ChoucairTest.Infrastructure.Middlewares;

public class LoginMiddleware : ILoginMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;
    public LoginMiddleware(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid GetIdUserLoggerd()
    {
        var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

        var valueId = identity?.FindFirst(ClaimTypes.Sid)?.Value;

        if (valueId is null) return Guid.Empty;

        return Guid.Parse(valueId);
    }
}
