using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Bogus;

namespace CommonTestUtilities.Entities;
public class InvoiceBuilder
{
    public static List<Invoice> Collection(User user, BarberShop barberShop, uint count = 2, uint beginFrom = 0)
    {
        var invoices = new List<Invoice>();

        if (count == 0)
            count = 1;

        var invoiceId = 1 + beginFrom;

        for (var i = 0;  i < count; i++)
        {
            var invoice = Build(user, barberShop);
            invoice.Id = invoiceId++;

            invoices.Add(invoice);
        }

        return invoices;
    }

    public static Invoice Build(User user, BarberShop barberShop)
    {
        return new Faker<Invoice>()
            .RuleFor(invoice => invoice.Id, _ => 1)
            .RuleFor(invoice => invoice.UserId, _ => user.Id)
            .RuleFor(invoice => invoice.Date, faker => faker.Date.Past())
            .RuleFor(invoice => invoice.BarberShopId, _ => barberShop.Id)
            .RuleFor(invoice => invoice.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(invoice => invoice.Title, faker => faker.Commerce.ProductName())
            .RuleFor(invoice => invoice.PaymentType, faker => faker.PickRandom<EPaymentType>())
            .RuleFor(invoice => invoice.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));
    }
}
