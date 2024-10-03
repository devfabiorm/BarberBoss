using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestInvoiceJsonBuilder
{
    public static RequestInvoiceJson Build()
    {
        return new Faker<RequestInvoiceJson>()
            .RuleFor(invoice => invoice.Title, faker => faker.Commerce.ProductName())
            .RuleFor(invoice => invoice.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(invoice => invoice.Date, faker => faker.Date.Past())
            .RuleFor(invoice => invoice.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(invoice => invoice.PaymentType, faker => faker.PickRandom<EPaymentType>());
    }
}
