using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess;
internal class BarberBossDbContext : DbContext
{
    public BarberBossDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BarberShop> BarberShops { get; set; }
}
