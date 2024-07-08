using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    public ResponseInvoiceJson Execute(RequestInvoiceJson request)
    {
        return new ResponseInvoiceJson { Title = "Testing" };
    }
}
