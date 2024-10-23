using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Invoices;
public interface IReadOnlyInvoiceRepository
{
    Task<Invoice?> GetById(long id, User user);
    Task<IList<Invoice>> GetAll(User user);
    Task<List<Invoice>> FilterByWeek(DateOnly date, User user, long shopId);
}
