using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoices;

namespace BarberBoss.Application.UseCases.Invoices.Update;
public class InvoiceUpdateUseCase : IInvoiceUpdateUseCase
{
    private readonly IUpdateOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public InvoiceUpdateUseCase(IUpdateOnlyInvoiceRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id, RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = await _repository.GetById(id);

        if (invoice is null) 
        { 
            //throw exception
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
            //throw an exception
        }
    }
}
