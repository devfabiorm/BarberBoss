using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Users;
public interface IReadOnlyUserRepository
{
    Task<User?> GetByEmail(string email);
    Task<bool> HasActiveEmail(string email);
}
