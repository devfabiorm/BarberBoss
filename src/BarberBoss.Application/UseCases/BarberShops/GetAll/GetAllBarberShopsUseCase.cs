using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.BarberShops;

namespace BarberBoss.Application.UseCases.BarberShops.GetAll;
public class GetAllBarberShopsUseCase : IGetAllBarberShopsUseCase
{
    private readonly IReadOnlyBarberShopRepository _repository;
    private readonly IMapper _mapper;

    public GetAllBarberShopsUseCase(IReadOnlyBarberShopRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseBarberShopsJson> Execute()
    {
        var shops = await _repository.GetAll();

        return new ResponseBarberShopsJson
        {
            BarberShops = _mapper.Map<IEnumerable<ResponseBarberShopShortJson>>(shops)
        };
    }
}
