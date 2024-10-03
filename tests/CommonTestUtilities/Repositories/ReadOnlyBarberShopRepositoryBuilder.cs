﻿using BarberBoss.Domain.Repositories.BarberShops;
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

    public IReadOnlyBarberShopRepository Build() => _repository.Object;
}
