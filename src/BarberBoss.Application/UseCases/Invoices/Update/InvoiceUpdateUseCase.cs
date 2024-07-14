using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoices.Update;
public class InvoiceUpdateUseCase : IInvoiceUpdateUseCase
{
    private readonly IUpdateOnlyInvoiceRepository _repository;

    public InvoiceUpdateUseCase(IUpdateOnlyInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(long id, RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = await _repository.GetById(id);

        if (invoice is null) 
        { 
            //throw exception
        }

        //do mapping

        _repository.Update(invoice);
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
