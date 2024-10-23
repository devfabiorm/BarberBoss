using BarberBoss.Domain.Entities;

namespace WebApi.Tests.Resources;
public class BarberShopIdentityManager
{
    private readonly BarberShop _shop;

    public BarberShopIdentityManager(BarberShop barberShop)
    {
        _shop = barberShop;
    }

    public long GetId() => _shop.Id;

    public string GetName() => _shop.Name;
}
