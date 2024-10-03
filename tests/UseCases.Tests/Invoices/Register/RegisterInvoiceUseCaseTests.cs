using BarberBoss.Application.UseCases.Invoices.Register;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.Invoices.Register;
public class RegisterInvoiceUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var request = RequestInvoiceJsonBuilder.Build();
        var useCase = CreateUseCase(request.BarberShopId);

        //Act
        var result = await useCase.Execute(request);

        //Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
        result.Description.Should().Be(request.Description);
        result.Amount.Should().Be(request.Amount);
        result.Date.Should().Be(request.Date);
        result.PaymentType.Should().Be(request.PaymentType);
    }

    private RegisterInvoiceUseCase CreateUseCase(long? shopId = null)
    {
        var repository = WriteOnlyInvoiceRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var barberShopReadOnlyRepo = new ReadOnlyBarberShopRepositoryBuilder().ShopExists(shopId).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new RegisterInvoiceUseCase(repository, mapper, barberShopReadOnlyRepo, unitOfWork);
    }
}
