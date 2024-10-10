using BarberBoss.Application.UseCases.BarberShops.GetAll;
using BarberBoss.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.BarberShops.GetAll;
public class GetAllBarberShopsUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        List<BarberShop> shops = BarberShopBuilder.Collection();

        var useCase = CreateUseCase(shops);

        //Act
        var result = await useCase.Execute();

        //Assert
        result.Should().NotBeNull();
        result.BarberShops.Should().NotBeEmpty();
    }

    private GetAllBarberShopsUseCase CreateUseCase(List<BarberShop> shops)
    {
        var repository = new ReadOnlyBarberShopRepositoryBuilder()
            .GetAll(shops)
            .Build();
        var mapper = MapperBuilder.Build();

        return new GetAllBarberShopsUseCase(repository, mapper);
    }
}
