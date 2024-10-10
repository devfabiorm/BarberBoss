using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestRegisterBarberShopJsonBuilder
{
    public static RequestRegisterBarberShopJson Build()
    {
        return new Faker<RequestRegisterBarberShopJson>()
            .RuleFor(shop => shop.Name, faker => faker.Company.CompanyName())
            .RuleFor(shop => shop.Address, faker => faker.Address.FullAddress());
    }
}
