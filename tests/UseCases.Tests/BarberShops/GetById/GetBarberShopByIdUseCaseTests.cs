using BarberBoss.Application.UseCases.BarberShops.GetById;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.BarberShops.GetById;
public class GetBarberShopByIdUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var shop = BarberShopBuilder.Build();

        var useCase = CreateUseCase(shop);

        //Act
        var result = await useCase.Execute(shop.Id);

        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(shop.Name);
        result.Address.Should().Be(shop.Address);
    }

    [Fact]
    public async Task Error_Invalid_Shop_Id()
    {
        var shop = BarberShopBuilder.Build();

        var useCase = CreateUseCase(shop);

        //Act
        var act = async () => await useCase.Execute(1000);

        //Assert
        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_SHOP_ID));
    }

    private GetBarberShopByIdUseCase CreateUseCase(BarberShop shop)
    {
        var repository = new ReadOnlyBarberShopRepositoryBuilder()
            .GetById(shop)
            .Build();
        var mapper = MapperBuilder.Build();

        return new GetBarberShopByIdUseCase(repository, mapper);
    }
}
