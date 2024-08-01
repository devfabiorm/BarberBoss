using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoices;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;
internal class InvoiceRepository : IWriteOnlyInvoiceRepository, IReadOnlyInvoiceRepository, IUpdateOnlyInvoiceRepository
{
    private const int NUMBER_OF_DAYS_BY_WEEK = 7;
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

    public async Task<List<Invoice>> FilterByWeek(DateOnly date)
    {
        var beginningOfTheWeek = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
        var startDate = new DateTime(year: beginningOfTheWeek.Year, month: beginningOfTheWeek.Month, day: beginningOfTheWeek.Day, hour: 0, minute: 0, second: 0);
        var endDate = startDate.AddDays(NUMBER_OF_DAYS_BY_WEEK - 1);

        return await _dbContext
            .Invoices
            .AsNoTracking()
            .Where(invoice => invoice.Date >= startDate && invoice.Date <= endDate)
            .OrderBy(invoice => invoice.Date)
            .ThenBy(invoice => invoice.Title)
            .ToListAsync();
    }
}
