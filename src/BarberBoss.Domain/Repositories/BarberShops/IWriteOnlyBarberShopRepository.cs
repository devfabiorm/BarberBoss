using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.BarberShops;
public interface IWriteOnlyBarberShopRepository
{
    Task Create(BarberShop shop);
}
