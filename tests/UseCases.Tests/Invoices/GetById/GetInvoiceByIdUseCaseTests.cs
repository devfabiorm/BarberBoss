using BarberBoss.Application.UseCases.Invoices.GetById;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Invoices.GetById;
public class GetInvoiceByIdUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var user = UserBuilder.Build();
        var barberShop = BarberShopBuilder.Build();
        var invoice = InvoiceBuilder.Build(user, barberShop);

        var useCase = CreateUseCase(invoice);

        //Act
        var result = await useCase.Execute(invoice.Id);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(invoice.Id);
        result.Title.Should().Be(invoice.Title);
        result.Description.Should().Be(invoice.Description);
        result.Amount.Should().Be(invoice.Amount);
        result.PaymentType.Should().Be((BarberBoss.Communication.Enums.EPaymentType)invoice.PaymentType);
    }

    [Fact]
    public async Task Error_Invoice_Not_Found()
    {
        //Arrange
        var user = UserBuilder.Build();
        var barberShop = BarberShopBuilder.Build();
        var invoice = InvoiceBuilder.Build(user, barberShop);

        var useCase = CreateUseCase(invoice);

        //Act
        var act = async () => await useCase.Execute(1000);

        //Assert
        var result = await act.Should().ThrowAsync<NotFoundException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVOICE_NOT_FOUND));
    }

    private GetInvoiceByIdUseCase CreateUseCase(Invoice invoice)
    {
        var repository = new ReadOnlyInvoiceRepositoryBuilder().GetById(invoice).Build();
        var mapper = MapperBuilder.Build();

        return new GetInvoiceByIdUseCase(repository, mapper); ;
    }
}
