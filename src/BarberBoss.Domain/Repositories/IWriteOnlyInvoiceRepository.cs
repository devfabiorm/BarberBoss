using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IWriteOnlyInvoiceRepository
{
    Task Create(Invoice invoice);
}
