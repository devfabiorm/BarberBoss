using BarberBoss.Application.UseCases.BarberShops.Delete;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.BarberShops.Delete;
public class DeleteBarberShopUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var shop = BarberShopBuilder.Build();

        var useCase = CreateUseCase(shop);

        //Act
        var act = async () => await useCase.Execute(shop.Id);

        //Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Invalid_Shop_Id()
    {
        //Arrange
        var shop = BarberShopBuilder.Build();

        var useCase = CreateUseCase(shop);

        //Act
        var act = async () => await useCase.Execute(1000);

        //Assert
        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_SHOP_ID));
    }

    private DeleteBarberShopUseCase CreateUseCase(BarberShop shop)
    {
        var repository = WriteOnlyBarberShopRepositoryBuilder.Build();
        var readOnlyRepository = new ReadOnlyBarberShopRepositoryBuilder()
            .GetById(shop)
            .Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new DeleteBarberShopUseCase(repository, readOnlyRepository, unitOfWork);
    }
}
