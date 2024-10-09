using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class WriteOnlyInvoiceRepositoryBuilder
{
    private readonly Mock<IWriteOnlyInvoiceRepository> _mock;

    public WriteOnlyInvoiceRepositoryBuilder()
    {
        _mock = new Mock<IWriteOnlyInvoiceRepository>();
    }

    public WriteOnlyInvoiceRepositoryBuilder Delete(User user, Invoice invoice)
    {
        if (invoice is not null)
        {
            _mock.Setup(repo => repo.Delete(invoice.Id, user)).ReturnsAsync(true);
        }

        return this;
    }

    public IWriteOnlyInvoiceRepository Build()
    {
       _mock.Setup(repo => repo.Create(It.IsAny<Invoice>())).Verifiable();

        return _mock.Object;
    }
}
