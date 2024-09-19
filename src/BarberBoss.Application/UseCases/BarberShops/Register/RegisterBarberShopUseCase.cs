using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShops;
using BarberBoss.Exception;

namespace BarberBoss.Application.UseCases.BarberShops.Register;
public class RegisterBarberShopUseCase : IRegisterBarberShopUseCase
{
    private readonly IWriteOnlyBarberShopRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterBarberShopUseCase(IWriteOnlyBarberShopRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseBarberShopJson> Execute(RequestRegisterBarberShopJson request)
    {
        Validate(request);

        var shop = _mapper.Map<BarberShop>(request);

        await _repository.Create(shop);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseBarberShopJson>(shop);
    }

    private void Validate(RequestRegisterBarberShopJson request)
    {
        var validator = new RegisterBarberShopValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
        }
    }
}
