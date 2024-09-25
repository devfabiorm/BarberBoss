using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Users;
public interface IUpdateOnlyUserRepository
{
    Task<User> GetById(long id);
    void Update(User user);
}
