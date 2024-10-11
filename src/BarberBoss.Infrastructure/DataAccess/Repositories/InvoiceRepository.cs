using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    public async Task<bool> Delete(long id, User user)
    {
        var invoice = await _dbContext.Invoices
            .SingleOrDefaultAsync(invoice => invoice.Id == id && invoice.UserId == user.Id);

        if (invoice == null) 
        {
            return false;
        }

        _dbContext.Invoices.Remove(invoice);

        return true;
    }

    public async Task<IList<Invoice>> GetAll(User user)
    {
        return await _dbContext
            .Invoices
            .AsNoTracking()
            .Where(invoice => invoice.UserId == user.Id)
            .ToListAsync();
    }

    async Task<Invoice?> IUpdateOnlyInvoiceRepository.GetById(long id, User user)
    {
        return await GetFullInvoice()
            .SingleOrDefaultAsync(invoice => invoice.Id == id && invoice.UserId == user.Id);
    }

    async Task<Invoice?> IReadOnlyInvoiceRepository.GetById(long id, User user)
    {
        return await GetFullInvoice()
            .AsNoTracking()
            .SingleOrDefaultAsync(invoice => invoice.Id == id && invoice.UserId == user.Id);
    }

    public void Update(Invoice invoice)
    {
        _dbContext.Invoices.Update(invoice);
    }

    public async Task<List<Invoice>> FilterByWeek(DateOnly date, User user)
    {
        var beginningOfTheWeek = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
        var startDate = new DateTime(year: beginningOfTheWeek.Year, month: beginningOfTheWeek.Month, day: beginningOfTheWeek.Day, hour: 0, minute: 0, second: 0);
        var dateToBeTheEnd = startDate.AddDays(NUMBER_OF_DAYS_BY_WEEK - 1);
        var endDate = new DateTime(year: dateToBeTheEnd.Year, month: dateToBeTheEnd.Month, day: dateToBeTheEnd.Day, hour: 23, minute: 59, second: 59);

        return await _dbContext
            .Invoices
            .AsNoTracking()
            .Where(invoice => invoice.Date >= startDate && invoice.Date <= endDate && invoice.UserId == user.Id)
            .OrderBy(invoice => invoice.Date)
            .ThenBy(invoice => invoice.Title)
            .ToListAsync();
    }

    private IIncludableQueryable<Invoice, BarberShop> GetFullInvoice()
    {
        return _dbContext
            .Invoices
            .Include(invoice => invoice.User)
            .Include(invoice => invoice.BarberShop);
    }
}
