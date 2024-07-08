using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.GetById;
public interface IGetInvoiceByIdUseCase
{
    ResponseInvoiceJson? Execute(long id);
}
