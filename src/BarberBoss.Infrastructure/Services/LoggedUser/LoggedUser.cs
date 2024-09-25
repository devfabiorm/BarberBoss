using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Security.Token;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberBoss.Infrastructure.Services.LoggedUser;
internal class LoggedUser : ILoggedUser
{
    private readonly BarberBossDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(BarberBossDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        var token = _tokenProvider.GetTokenFromRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.ReadJwtToken(token);

        var userIdentification = securityToken.Claims.First(claim =>  claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(userIdentification));

    }
}
