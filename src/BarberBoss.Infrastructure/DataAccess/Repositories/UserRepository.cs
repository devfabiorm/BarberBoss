using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Users;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class UserRepository : IWriteOnlyUserRepository
{
    private readonly BarberBossDbContext _dbContext;

    public UserRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}

