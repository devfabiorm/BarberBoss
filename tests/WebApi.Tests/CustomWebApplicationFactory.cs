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
    public InvoiceIdentityManager Invoice { get; private set; } = default!;
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

                var user = InitializeUserDatabase(dbContext, passwordEncrypter, accessTokenGenerator);
                var shop = InitializeBarberShopDatabase(dbContext);
                InitializeInvoiceDatabase(dbContext, user, shop);

            });
    }

    private User InitializeUserDatabase(BarberBossDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
       var user =  UserBuilder.Build();

        var rawPassword = user.Password;
        
        user.Password = passwordEncrypter.Encrypt(user.Password);

        var token = accessTokenGenerator.Generate(user);

        User_TeamMember = new UserIdentityManager(user, rawPassword, token);

        dbContext.Users.Add(user);

        dbContext.SaveChanges();

        return user;
    } 

    private BarberShop InitializeBarberShopDatabase(BarberBossDbContext dbContext)
    {
        var shop = BarberShopBuilder.Build();
        
        Shop = new BarberShopIdentityManager(shop);

        dbContext.BarberShops.Add(shop);

        dbContext.SaveChanges();

        return shop;
    }

    private void InitializeInvoiceDatabase(BarberBossDbContext dbContext, User  user, BarberShop shop)
    {
        var invoices = InvoiceBuilder.Collection(user, shop);

        Invoice = new InvoiceIdentityManager(invoices[0]);

        dbContext.Invoices.AddRange(invoices);

        dbContext.SaveChanges();
    }
}
