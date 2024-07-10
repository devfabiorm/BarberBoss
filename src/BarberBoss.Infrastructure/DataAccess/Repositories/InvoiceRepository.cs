using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class InvoiceRepository : IWriteOnlyInvoiceRepository
{
    private readonly BarberBossDbContext _dbContext;
    public InvoiceRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Invoice invoice)
    {
       await _dbContext.Invoices.AddAsync(invoice);
    }
}
