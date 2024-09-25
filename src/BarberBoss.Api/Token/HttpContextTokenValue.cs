using BarberBoss.Domain.Security.Token;

namespace BarberBoss.Api.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }

    public string GetTokenFromRequest()
    {
        var token = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return token["Bearer ".Length..];
    }
}
