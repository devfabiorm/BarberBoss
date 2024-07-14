using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IUpdateOnlyInvoiceRepository
{
    void Update(Invoice invoice);
    Task<Invoice?> GetById(long id);
}
