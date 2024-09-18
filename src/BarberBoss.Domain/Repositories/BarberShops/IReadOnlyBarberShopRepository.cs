using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.BarberShops;
public interface IReadOnlyBarberShopRepository
{
    Task<bool> ShopExist(long id);
}
