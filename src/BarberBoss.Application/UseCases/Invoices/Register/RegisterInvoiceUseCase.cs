using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{

    public ResponseInvoiceJson Execute(RequestInvoiceJson request)
    {
        Validate(request);

        return new ResponseInvoiceJson { Title = "Testing" };
    }

    private void Validate(RequestInvoiceJson request)
    {
        var validator = new InvoiceValidator();

        var result = validator.Validate(request);

        if (!result.IsValid) 
        { 
            //throw an exception
        }
    }
}
