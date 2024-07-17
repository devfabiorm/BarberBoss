using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Invoices;
public interface IWriteOnlyInvoiceRepository
{
    Task Create(Invoice invoice);
    Task<bool> Delete(long id);
}
