using BarberBoss.Domain.Constants;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Token;
using BarberBoss.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Tests.Resources;

namespace WebApi.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public UserIdentityManager User_TeamMember { get; private set; } = default!;
    public UserIdentityManager User_Admin { get; private set; } = default!;
    public InvoiceIdentityManager Invoice_TeamMember { get; private set; } = default!;
    public InvoiceIdentityManager Invoice_Admin { get; private set; } = default!;
    public BarberShopIdentityManager Shop { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                services.AddDbContext<BarberBossDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<BarberBossDbContext>();
                var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();


                InitializeDatabase(dbContext, passwordEncrypter, accessTokenGenerator);
            });
    }

    private void InitializeDatabase(BarberBossDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = SeedUserTeamMember(dbContext, passwordEncrypter, accessTokenGenerator);
        var shop = SeedBarberShop(dbContext);
        var invoice = SeedInvoice(dbContext, user, shop, beginFrom: 0);
        Invoice_TeamMember = new InvoiceIdentityManager(invoice);

        var adminUser = SeedUserAdmin(dbContext, passwordEncrypter, accessTokenGenerator);
        invoice = SeedInvoice(dbContext, adminUser, shop, beginFrom: 3);
        Invoice_Admin = new InvoiceIdentityManager(invoice);

        dbContext.SaveChanges();
    }

    private User SeedUserTeamMember(BarberBossDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id = 1;

        var rawPassword = user.Password;

        user.Password = passwordEncrypter.Encrypt(user.Password);

        var token = accessTokenGenerator.Generate(user);

        User_TeamMember = new UserIdentityManager(user, rawPassword, token);

        dbContext.Users.Add(user);

        return user;
    }

    private User SeedUserAdmin(BarberBossDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build(Roles.Admin);
        user.Id = 2;

        var rawPassword = user.Password;

        user.Password = passwordEncrypter.Encrypt(user.Password);

        var token = accessTokenGenerator.Generate(user);

        User_Admin = new UserIdentityManager(user, rawPassword, token);

        dbContext.Users.Add(user);

        return user;
    }

    private BarberShop SeedBarberShop(BarberBossDbContext dbContext)
    {
        var shop = BarberShopBuilder.Build();

        Shop = new BarberShopIdentityManager(shop);

        dbContext.BarberShops.Add(shop);

        return shop;
    }

    private Invoice SeedInvoice(BarberBossDbContext dbContext, User user, BarberShop shop, uint beginFrom)
    {
        var invoices = InvoiceBuilder.Collection(user, shop, beginFrom: beginFrom);

        dbContext.Invoices.AddRange(invoices);

        return invoices[0];
    }
}
