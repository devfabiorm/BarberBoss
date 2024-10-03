using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class WriteOnlyInvoiceRepositoryBuilder
{
    public static IWriteOnlyInvoiceRepository Build()
    {
        var mock = new Mock<IWriteOnlyInvoiceRepository>();

        mock.Setup(repo => repo.Create(It.IsAny<Invoice>())).Verifiable();

        return mock.Object;
    }
}
