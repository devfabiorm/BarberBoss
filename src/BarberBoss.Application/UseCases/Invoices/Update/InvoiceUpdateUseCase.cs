using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Invoices.Update;
public class InvoiceUpdateUseCase : IInvoiceUpdateUseCase
{
    public void Execute(long id, RequestInvoiceJson request)
    {
        throw new NotImplementedException();
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
