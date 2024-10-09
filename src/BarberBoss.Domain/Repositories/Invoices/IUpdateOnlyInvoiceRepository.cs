using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Invoices;
public interface IUpdateOnlyInvoiceRepository
{
    void Update(Invoice invoice);
    Task<Invoice?> GetById(long id, User user);
}
