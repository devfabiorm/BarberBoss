using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;

public class UpdateOnlyInvoiceRepositoryBuilder
{
    private readonly Mock<IUpdateOnlyInvoiceRepository> _mock;

    public UpdateOnlyInvoiceRepositoryBuilder()
    {
        _mock = new Mock<IUpdateOnlyInvoiceRepository>();
    }

    public UpdateOnlyInvoiceRepositoryBuilder GetById(User user, Invoice invoice)
    {
        if (invoice is not null)
        {
            _mock.Setup(repo => repo.GetById(invoice.Id, user)).ReturnsAsync(invoice);
        }

        return this;
    }

    public IUpdateOnlyInvoiceRepository Build() => _mock.Object;
}

