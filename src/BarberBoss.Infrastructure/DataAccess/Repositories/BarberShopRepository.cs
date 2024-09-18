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

    public async Task<bool> ShopExist(long id)
    {
        return await _dbContext.BarberShops.AnyAsync(shop => shop.Id == id);
    }
}
