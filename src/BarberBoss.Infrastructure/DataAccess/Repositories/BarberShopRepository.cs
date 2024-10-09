using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.BarberShops;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class BarberShopRepository : IWriteOnlyBarberShopRepository, IReadOnlyBarberShopRepository
{
    private readonly BarberBossDbContext _dbContext;

    public BarberShopRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(BarberShop shop)
    {
        await _dbContext.BarberShops.AddAsync(shop);
    }

    public void Delete(BarberShop shop)
    {
        _dbContext.BarberShops.Remove(shop);
    }

    public async Task<IEnumerable<BarberShop>> GetAll()
    {
        return await _dbContext.BarberShops.ToListAsync();
    }

    public async Task<BarberShop?> GetById(long id)
    {
        return await _dbContext.BarberShops.SingleOrDefaultAsync(shop => shop.Id == id);
    }

    public async Task<bool> ShopExist(long id)
    {
        return await _dbContext.BarberShops.AnyAsync(shop => shop.Id == id);
    }
}
