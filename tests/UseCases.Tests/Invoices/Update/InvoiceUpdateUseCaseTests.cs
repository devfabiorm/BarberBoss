using BarberBoss.Application.UseCases.Invoices.Update;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.Invoices.Update;
public class InvoiceUpdateUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var barberShop = BarberShopBuilder.Build();
        var user = UserBuilder.Build();
        var invoice = InvoiceBuilder.Build(user, barberShop);
        var request = RequestInvoiceJsonBuilder.Build();
        var useCase = CreateUseCase(invoice);

        //Act
        var act = async () => await useCase.Execute(invoice.Id, request);

        //Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_Invoice_Not_Found()
    {
        var barberShop = BarberShopBuilder.Build();
        var user = UserBuilder.Build();
        var invoice = InvoiceBuilder.Build(user, barberShop);
        var request = RequestInvoiceJsonBuilder.Build();
        var useCase = CreateUseCase(invoice);

        //Act
        var act = async () => await useCase.Execute(1000, request);

        //Assert
        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVOICE_NOT_FOUND));
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        //Arrange
        var barberShop = BarberShopBuilder.Build();
        var user = UserBuilder.Build();
        var invoice = InvoiceBuilder.Build(user, barberShop);
        var request = RequestInvoiceJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(invoice);

        //Act
        var act = async () => await useCase.Execute(1000, request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.REQUIRED_TITLE));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-30)]
    [InlineData(-55)]
    [InlineData(-60)]
    public async Task Error_Amount_Equal_Or_Less_Zero(decimal amount)
    {
        //Arrange
        var barberShop = BarberShopBuilder.Build();
        var user = UserBuilder.Build();
        var invoice = InvoiceBuilder.Build(user, barberShop);
        var request = RequestInvoiceJsonBuilder.Build();
        request.Amount = amount;

        var useCase = CreateUseCase(invoice);

        //Act
        var act = async () => await useCase.Execute(1000, request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVOICE_AMOUNT_GREATER_THAN_ZERO));
    }

    public InvoiceUpdateUseCase CreateUseCase(Invoice invoice)
    {
        var repository = new UpdateOnlyInvoiceRepositoryBuilder().GetById(invoice).Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new InvoiceUpdateUseCase(repository, mapper, unitOfWork);
    }
}
