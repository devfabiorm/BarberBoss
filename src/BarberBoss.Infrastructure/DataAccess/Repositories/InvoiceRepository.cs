using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class InvoiceRepository : IWriteOnlyInvoiceRepository, IReadOnlyInvoiceRepository
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

    public async Task<IList<Invoice>> GetAll()
    {
        return await _dbContext
            .Invoices
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Invoice?> GetById(long id)
    {
        return await _dbContext
            .Invoices
            .AsNoTracking()
            .SingleOrDefaultAsync(invoice => invoice.Id == id);
    }
}
