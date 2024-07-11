using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoices.GetById;
internal class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;

    public GetInvoiceByIdUseCase(IReadOnlyInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseInvoiceJson?> Execute(long id)
    {
        var invoice = await _repository.GetById(id);
        return new ResponseInvoiceJson { Title = "Test" };
    }
}
