using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Invoices.GetAll;
using BarberBoss.Application.UseCases.Invoices.GetById;
using BarberBoss.Application.UseCases.Invoices.Register;
using BarberBoss.Application.UseCases.Invoices.Update;
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
        services.AddScoped<IRegisterInvoiceUseCase, RegisterInvoiceUseCase>();
        services.AddScoped<IGetAllInvoicesUseCase, GetAllInvoicesUseCase>();
        services.AddScoped<IGetInvoiceByIdUseCase, GetInvoiceByIdUseCase>();
        services.AddScoped<IInvoiceUpdateUseCase, InvoiceUpdateUseCase>();
    }

    private static void AddMapper(this IServiceCollection services) 
    { 
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
