﻿using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.BarberShops.Delete;
using BarberBoss.Application.UseCases.BarberShops.GetAll;
using BarberBoss.Application.UseCases.BarberShops.GetById;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Application.UseCases.Invoices.Delete;
using BarberBoss.Application.UseCases.Invoices.GetAll;
using BarberBoss.Application.UseCases.Invoices.GetById;
using BarberBoss.Application.UseCases.Invoices.Register;
using BarberBoss.Application.UseCases.Invoices.Report.Excel;
using BarberBoss.Application.UseCases.Invoices.Report.Pdf;
using BarberBoss.Application.UseCases.Invoices.Update;
using BarberBoss.Application.UseCases.Login.DoLogin;
using BarberBoss.Application.UseCases.Users.Delete;
using BarberBoss.Application.UseCases.Users.Profile;
using BarberBoss.Application.UseCases.Users.Register;
using BarberBoss.Application.UseCases.Users.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddMapper(services);
    }

    private static void AddUseCases(this IServiceCollection services) 
    { 
        services.AddScoped<IDeleteInvoiceUseCase, DeleteInvoiceUseCase>();
        services.AddScoped<IRegisterInvoiceUseCase, RegisterInvoiceUseCase>();
        services.AddScoped<IGetAllInvoicesUseCase, GetAllInvoicesUseCase>();
        services.AddScoped<IGetInvoiceByIdUseCase, GetInvoiceByIdUseCase>();
        services.AddScoped<IInvoiceUpdateUseCase, InvoiceUpdateUseCase>();
        services.AddScoped<IGenerateInvoiceReportExcelUseCase, GenerateInvoiceReportExcelUseCase>();
        services.AddScoped<IGenerateInvoiceReportPdfUseCase, GenerateInvoiceReportPdfUseCase>();
        services.AddScoped<IRegisterBarberShopUseCase, RegisterBarberShopUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetAllBarberShopsUseCase, GetAllBarberShopsUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
        services.AddScoped<IGetBarberShopByIdUseCase, GetBarberShopByIdUseCase>();
        services.AddScoped<IDeleteBarberShopUseCase, DeleteBarberShopUseCase>();
        services.AddScoped<IGetBarberShopByIdUseCase, GetBarberShopByIdUseCase>();
    }

    private static void AddMapper(this IServiceCollection services) 
    { 
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
