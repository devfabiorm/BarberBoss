using BarberBoss.Domain.Entities;
using Bogus;

namespace CommonTestUtilities.Entities;
public class BarberShopBuilder
{
    public static BarberShop Build()
    {
        return new Faker<BarberShop>()
            .RuleFor(shop => shop.Name, faker => faker.Company.CompanyName())
            .RuleFor(shop => shop.Address, faker => faker.Address.FullAddress())
            .RuleFor(shop => shop.Phone, faker => faker.Phone.PhoneNumber());
    }
}
