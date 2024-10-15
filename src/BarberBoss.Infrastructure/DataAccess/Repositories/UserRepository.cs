using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class UserRepository : IWriteOnlyUserRepository, IReadOnlyUserRepository, IUpdateOnlyUserRepository
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

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public async Task<User> GetById(long id)
    {
        return await _dbContext.Users.FirstAsync(user => user.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> HasActiveEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }
}

