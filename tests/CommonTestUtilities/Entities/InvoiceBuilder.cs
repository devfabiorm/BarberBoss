using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Bogus;

namespace CommonTestUtilities.Entities;
public class InvoiceBuilder
{
    public static Invoice Build(User user, BarberShop barberShop)
    {
        return new Faker<Invoice>()
            .RuleFor(invoice => invoice.Id, _ => 1)
            .RuleFor(invoice => invoice.UserId, _ => user.Id)
            .RuleFor(invoice => invoice.Date, faker => faker.Date.Past())
            .RuleFor(invoice => invoice.BarberShopId, _ => barberShop.Id)
            .RuleFor(invoice => invoice.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(invoice => invoice.Title, faker => faker.Commerce.ProductName())
            .RuleFor(invoice => invoice.PaymentType, faker => faker.PickRandom<EPaymentType>());
    }
}
