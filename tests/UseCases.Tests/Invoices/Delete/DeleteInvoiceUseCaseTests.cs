using BarberBoss.Application.UseCases.Invoices.Delete;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Invoices.Delete;
public class DeleteInvoiceUseCaseTests
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
        var act = async () => await useCase.Execute(invoice.Id);

        //Assert
        await act.Should().NotThrowAsync();
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

    private DeleteInvoiceUseCase CreateUseCase(Invoice invoice)
    {
        var repository = new WriteOnlyInvoiceRepositoryBuilder().Delete(invoice).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new DeleteInvoiceUseCase(repository, unitOfWork);
    }
}
