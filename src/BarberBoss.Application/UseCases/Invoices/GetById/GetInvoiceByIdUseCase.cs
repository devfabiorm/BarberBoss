using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Exception.Messages;
using BarberBoss.Exception;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.Invoices.GetById;
public class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetInvoiceByIdUseCase(
        IReadOnlyInvoiceRepository repository,
        IMapper mapper,
        ILoggedUser user)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = user;
    }

    public async Task<ResponseInvoiceJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var invoice = await _repository.GetById(id, loggedUser);

        if (invoice is null) 
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        return _mapper.Map<ResponseInvoiceJson>(invoice);
    }
}
