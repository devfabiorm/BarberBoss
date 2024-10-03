using BarberBoss.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ReadOnlyInvoiceRepositoryBuilder
{
    public static IReadOnlyInvoiceRepository Build()
    {
        var mock = new Mock<IReadOnlyInvoiceRepository>();

        return mock.Object;
    }
}
