using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.GetAll;
public interface IGetAllInvoicesUseCase
{
    Task<ResponseInvoicesJson> Execute();
}
