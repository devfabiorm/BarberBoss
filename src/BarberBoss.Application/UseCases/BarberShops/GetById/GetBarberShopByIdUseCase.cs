using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.BarberShops;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;

namespace BarberBoss.Application.UseCases.BarberShops.GetById;
public class GetBarberShopByIdUseCase : IGetBarberShopByIdUseCase
{
    private readonly IReadOnlyBarberShopRepository _repository;
    private readonly IMapper _mapper;

    public GetBarberShopByIdUseCase(
        IReadOnlyBarberShopRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBarberShopJson> Execute(long id)
    {
        var shop = await _repository.GetById(id);

        if (shop == null) 
        {
            throw new NotFoundException(ResourceErrorMessages.INVALID_SHOP_ID);
        }

        return _mapper.Map<ResponseBarberShopJson>(shop);
    }
}
