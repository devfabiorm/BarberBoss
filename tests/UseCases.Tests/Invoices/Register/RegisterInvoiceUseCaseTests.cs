using BarberBoss.Application.UseCases.Invoices.Register;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
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

    [Fact]
    public async Task Error_Title_Empty()
    {
        //Arrange
        var request = RequestInvoiceJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(request.BarberShopId);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.REQUIRED_TITLE));
    }

    [Fact]
    public async Task Error_Future_Date()
    {
        //Arrange
        var request = RequestInvoiceJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        var useCase = CreateUseCase(request.BarberShopId);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVOICE_DATE_NEEDS_OLDER_THAN_CURRENT));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-55)]
    public async Task Error_Amount_Zero_Or_Below(decimal amount)
    {
        //Arrange
        var request = RequestInvoiceJsonBuilder.Build();
        request.Amount = amount;

        var useCase = CreateUseCase(request.BarberShopId);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVOICE_AMOUNT_GREATER_THAN_ZERO));
    }

    [Fact]
    public async Task Error_Invalid_PaymentType()
    {
        //Arrange
        var request = RequestInvoiceJsonBuilder.Build();
        request.PaymentType = (BarberBoss.Communication.Enums.EPaymentType)100;

        var useCase = CreateUseCase(request.BarberShopId);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_PAYMENT_TYPE));
    }

    [Fact]
    public async Task Inexistent_Shop()
    {
        //Arrange
        var request = RequestInvoiceJsonBuilder.Build();

        var useCase = CreateUseCase();

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_SHOP_ID));
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
