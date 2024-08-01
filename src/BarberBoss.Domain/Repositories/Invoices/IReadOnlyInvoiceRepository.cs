using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Invoices;
public interface IReadOnlyInvoiceRepository
{
    Task<Invoice?> GetById(long id);
    Task<IList<Invoice>> GetAll();
    Task<List<Invoice>> FilterByWeek(DateOnly date);
}
