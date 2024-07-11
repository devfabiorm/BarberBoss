using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoices.GetAll;
internal class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;

    public GetAllInvoicesUseCase(IReadOnlyInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseInvoicesJson> Execute()
    {
        var invoices = await _repository.GetAll();
        return new ResponseInvoicesJson();
    }
}
