using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Invoices;

namespace BarberBoss.Application.UseCases.Invoices.GetAll;
internal class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public GetAllInvoicesUseCase(IReadOnlyInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseInvoicesJson> Execute()
    {
        var invoices = await _repository.GetAll();

        if (invoices.Count == 0) 
        {
            return new ResponseInvoicesJson { Invoices = [] };
        }

        var shortInvoices = _mapper.Map<List<ResponseInvoiceShortJson>>(invoices);

        return new ResponseInvoicesJson
        {
            Invoices = shortInvoices
        };
    }
}
