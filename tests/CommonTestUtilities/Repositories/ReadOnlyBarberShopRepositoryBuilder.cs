using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.BarberShops;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ReadOnlyBarberShopRepositoryBuilder
{
    private readonly Mock<IReadOnlyBarberShopRepository> _repository;

    public ReadOnlyBarberShopRepositoryBuilder()
    {
        _repository = new Mock<IReadOnlyBarberShopRepository>();
    }

    public ReadOnlyBarberShopRepositoryBuilder ShopExists(long? shopId)
    {
        if (shopId.HasValue) 
        { 
            _repository.Setup(shop => shop.ShopExist(shopId.Value)).ReturnsAsync(true);
        }

        return this;
    }

    public ReadOnlyBarberShopRepositoryBuilder GetAll(List<BarberShop> shops)
    {
        _repository.Setup(repo => repo.GetAll())
            .ReturnsAsync(shops);

        return this;
    }

    public ReadOnlyBarberShopRepositoryBuilder GetById(BarberShop shop)
    {
        if (shop is not null)
        {
            _repository.Setup(repo => repo.GetById(shop.Id)).ReturnsAsync(shop);
        }

        return this;
    }

    public IReadOnlyBarberShopRepository Build() => _repository.Object;
}
