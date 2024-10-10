using BarberBoss.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities;
public class BarberShopBuilder
{
    public static BarberShop Build()
    {
        return new Faker<BarberShop>()
            .RuleFor(shop => shop.Id, _ => 1)
            .RuleFor(shop => shop.Name, faker => faker.Company.CompanyName())
            .RuleFor(shop => shop.Address, faker => faker.Address.FullAddress())
            .RuleFor(shop => shop.Phone, faker => faker.Phone.PhoneNumber());
    }

    public static List<BarberShop> Collection(int count = 2)
    {
        var shops = new List<BarberShop>();

        if (count == 0)
            count = 1;

        var barberShopId = 1;

        for (int i = 0;  i < count; i++)
        {
            var shop = Build();
            shop.Id = barberShopId++;

            shops.Add(shop);
        }

        return shops;
    }
}
