using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Invoices.GetAll;
internal class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    public ResponseInvoicesJson Execute()
    {
        return new ResponseInvoicesJson();
    }
}
