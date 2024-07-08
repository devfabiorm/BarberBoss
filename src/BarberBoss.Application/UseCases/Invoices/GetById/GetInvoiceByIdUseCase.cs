using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.GetById;
internal class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
{
    public ResponseInvoiceJson? Execute(long id)
    {
        return new ResponseInvoiceJson { Title = "Test" };
    }
}
