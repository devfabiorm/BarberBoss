using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.BarberShops;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;

namespace BarberBoss.Application.UseCases.BarberShops.GetById;
public class GetBarberShopByIdUseCase : IGetBarberShopByIdUseCase
{
    private readonly IReadOnlyBarberShopRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetBarberShopByIdUseCase(
        IReadOnlyBarberShopRepository repository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseBarberShopJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var shop = await _repository.GetById(id);

        if (shop == null) 
        {
            throw new NotFoundException(ResourceErrorMessages.INVALID_SHOP_ID);
        }

        return _mapper.Map<ResponseBarberShopJson>(shop);
    }
}
