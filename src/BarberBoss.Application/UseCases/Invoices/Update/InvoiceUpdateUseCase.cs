using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;

namespace BarberBoss.Application.UseCases.Invoices.Update;
public class InvoiceUpdateUseCase : IInvoiceUpdateUseCase
{
    private readonly IUpdateOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public InvoiceUpdateUseCase(
        IUpdateOnlyInvoiceRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id, RequestInvoiceJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();
        var invoice = await _repository.GetById(id, loggedUser);

        if (invoice is null) 
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        _mapper.Map(request, invoice);

        _repository.Update(invoice);

        await _unitOfWork.Commit();
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
