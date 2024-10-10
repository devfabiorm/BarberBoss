using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.BarberShops.Register;
public class RegisterBarberShopUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var request = RequestRegisterBarberShopJsonBuilder.Build();

        var useCase = CreateUseCase();

        //Act
        var result = await useCase.Execute(request);

        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(result.Name);
        result.Address.Should().Be(result.Address);
    }

    [Fact]
    public async Task Error_Invalid_Name()
    {
        //Arrange
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_SHOP_NAME));
    }

    [Fact]
    public async Task Error_Invalid_Address()
    {
        //Arrange
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Address = string.Empty;

        var useCase = CreateUseCase();

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_SHOP_ADDRESS));
    }

    private RegisterBarberShopUseCase CreateUseCase()
    {
        var repository = WriteOnlyBarberShopRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new RegisterBarberShopUseCase(repository, mapper, unitOfWork);
    }
}
