using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.BarberShops;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;

namespace BarberBoss.Application.UseCases.BarberShops.Delete;
public class DeleteBarberShopUseCase : IDeleteBarberShopUseCase
{
    private readonly IWriteOnlyBarberShopRepository _repository;
    private readonly IReadOnlyBarberShopRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBarberShopUseCase(
        IWriteOnlyBarberShopRepository repository,
        IReadOnlyBarberShopRepository readOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var barberShop = await _readOnlyRepository.GetById(id);

        if (barberShop is null)
        {
            throw new NotFoundException(ResourceErrorMessages.INVALID_SHOP_ID);
        }
        
        _repository.Delete(barberShop);

        await _unitOfWork.Commit();
    }
}
