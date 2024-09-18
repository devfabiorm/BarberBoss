using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShops;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.Invoices.Register;
public class RegisterInvoiceUseCase : IRegisterInvoiceUseCase
{
    private readonly IWriteOnlyInvoiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IReadOnlyBarberShopRepository _barberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterInvoiceUseCase(IWriteOnlyInvoiceRepository repository, IMapper mapper, IReadOnlyBarberShopRepository barberRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _barberRepository = barberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseInvoiceJson> Execute(RequestInvoiceJson request)
    {
        await Validate(request);

        var invoice = _mapper.Map<Invoice>(request);

        await _repository.Create(invoice);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseInvoiceJson>(invoice);
    }

    private async Task Validate(RequestInvoiceJson request)
    {
        var validator = new InvoiceValidator();

        var result = validator.Validate(request);

        var isThereAnyShop = await _barberRepository.ShopExist(request.BarberShopId);

        if (!isThereAnyShop)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.INVALID_SHOP_ID));
        }

        if (!result.IsValid) 
        { 
            throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
        }
    }
}
