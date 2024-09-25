using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Security.Token;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
