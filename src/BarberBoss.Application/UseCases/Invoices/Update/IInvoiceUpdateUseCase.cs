using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Invoices.Update;
public interface IInvoiceUpdateUseCase
{
    void Execute(long id, RequestInvoiceJson request);
}
