using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Exception;

namespace BarberBoss.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    private readonly IWriteOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterInvoiceUseCase(IWriteOnlyInvoiceRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseInvoiceJson> Execute(RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = _mapper.Map<Invoice>(request);

        await _repository.Create(invoice);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseInvoiceJson>(invoice);
    }

    private void Validate(RequestInvoiceJson request)
    {
        var validator = new InvoiceValidator();

        var result = validator.Validate(request);

        if (!result.IsValid) 
        { 
            throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
        }
    }
}
