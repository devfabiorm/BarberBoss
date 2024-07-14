using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class InvoiceRepository : IWriteOnlyInvoiceRepository, IReadOnlyInvoiceRepository, IUpdateOnlyInvoiceRepository
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

    public async Task<bool> Delete(long id)
    {
        var invoice = await _dbContext.Invoices.SingleOrDefaultAsync(x => x.Id == id);

        if (invoice == null) 
        {
            return false;
        }

        _dbContext.Invoices.Remove(invoice);

        return true;
    }

    public async Task<IList<Invoice>> GetAll()
    {
        return await _dbContext
            .Invoices
            .AsNoTracking()
            .ToListAsync();
    }

    async Task<Invoice?> IUpdateOnlyInvoiceRepository.GetById(long id)
    {
        return await _dbContext
            .Invoices
            .SingleOrDefaultAsync(invoice => invoice.Id == id);
    }

    async Task<Invoice?> IReadOnlyInvoiceRepository.GetById(long id)
    {
        return await _dbContext
            .Invoices
            .AsNoTracking()
            .SingleOrDefaultAsync(invoice => invoice.Id == id);
    }

    public void Update(Invoice invoice)
    {
        _dbContext.Invoices.Update(invoice);
    }
}
