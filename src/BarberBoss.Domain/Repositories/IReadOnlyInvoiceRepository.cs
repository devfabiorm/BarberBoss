using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories;
public interface IReadOnlyInvoiceRepository
{
    Task<Invoice?> GetById(long id);
    Task<IList<Invoice>> GetAll();
}
