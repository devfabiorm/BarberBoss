using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ReadOnlyInvoiceRepositoryBuilder
{
    private readonly Mock<IReadOnlyInvoiceRepository> _repository;

    public ReadOnlyInvoiceRepositoryBuilder()
    {
          _repository = new Mock<IReadOnlyInvoiceRepository>();
    }

    public ReadOnlyInvoiceRepositoryBuilder GetAll(User user, List<Invoice> invoices)
    {
        _repository.Setup(repo => repo.GetAll(user)).ReturnsAsync(invoices);

        return this;
    }

    public ReadOnlyInvoiceRepositoryBuilder GetById(User user, Invoice invoice)
    {
        if (invoice is not null)
        {
            _repository.Setup(repo => repo.GetById(invoice.Id, user)).ReturnsAsync(invoice);
        }

        return this;
    }

    public ReadOnlyInvoiceRepositoryBuilder FilterByWeek(User user, List<Invoice> invoices)
    {
        _repository.Setup(repo => repo.FilterByWeek(It.IsAny<DateOnly>(), user, It.IsAny<long>())).ReturnsAsync(invoices);
        
        return this;
    }

    public IReadOnlyInvoiceRepository Build() =>_repository.Object;
}
