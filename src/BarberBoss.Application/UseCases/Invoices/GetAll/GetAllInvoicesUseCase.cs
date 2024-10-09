using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.Invoices.GetAll;
public class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetAllInvoicesUseCase(
        IReadOnlyInvoiceRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseInvoicesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        var invoices = await _repository.GetAll(loggedUser);

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
