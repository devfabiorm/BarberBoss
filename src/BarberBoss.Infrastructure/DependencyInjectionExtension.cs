﻿using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShops;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Token;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using BarberBoss.Infrastructure.Extensions;
using BarberBoss.Infrastructure.Security.Cryptography;
using BarberBoss.Infrastructure.Security.Tokens;
using BarberBoss.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        AddToken(services, configuration);
        AddRepositories(services);

        if(!configuration.IsTestEnvironment())
        {
            AddDbContext(services, configuration);
        }
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationInMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationInMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IWriteOnlyInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IReadOnlyInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IUpdateOnlyInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IWriteOnlyBarberShopRepository, BarberShopRepository>();
        services.AddScoped<IReadOnlyBarberShopRepository, BarberShopRepository>();
        services.AddScoped<IWriteOnlyUserRepository, UserRepository>();
        services.AddScoped<IReadOnlyUserRepository, UserRepository>();
        services.AddScoped<IUpdateOnlyUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
