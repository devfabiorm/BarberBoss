using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Invoices.Update;
public interface IInvoiceUpdateUseCase
{
    Task Execute(long id, RequestInvoiceJson request);
}
