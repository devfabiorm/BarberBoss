using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    private readonly IWriteOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public RegisterInvoiceUseCase(IWriteOnlyInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseInvoiceJson> Execute(RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = _mapper.Map<Invoice>(request);

        await _repository.Create(invoice);

        return _mapper.Map<ResponseInvoiceJson>(invoice);
    }

    private void Validate(RequestInvoiceJson request)
    {
        var validator = new InvoiceValidator();

        var result = validator.Validate(request);

        if (!result.IsValid) 
        { 
            //throw an exception
        }
    }
}
