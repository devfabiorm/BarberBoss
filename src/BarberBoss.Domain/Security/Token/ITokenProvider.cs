namespace BarberBoss.Domain.Security.Token;
public interface ITokenProvider
{
    string GetTokenFromRequest();
}
