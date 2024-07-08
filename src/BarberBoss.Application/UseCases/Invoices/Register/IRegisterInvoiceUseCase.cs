using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.Register;
public interface IRegisterInvoiceUseCase
{
    ResponseInvoiceJson Execute(RequestInvoiceJson request);
}
