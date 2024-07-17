using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Invoices;

namespace BarberBoss.Application.UseCases.Invoices.GetById;
internal class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public GetInvoiceByIdUseCase(IReadOnlyInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseInvoiceJson?> Execute(long id)
    {
        var invoice = await _repository.GetById(id);

        if (invoice is null) 
        { 
            //Throw exception
        }

        return _mapper.Map<ResponseInvoiceJson>(invoice);
    }
}
