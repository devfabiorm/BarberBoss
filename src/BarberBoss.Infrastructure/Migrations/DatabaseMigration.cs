using BarberBoss.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure.Migrations;
public static class DatabaseMigration
{
    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<BarberBossDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
