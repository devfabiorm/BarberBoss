using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Users;
public interface IWriteOnlyUserRepository
{
    Task Create(User user);
    void Delete(User user);
}
