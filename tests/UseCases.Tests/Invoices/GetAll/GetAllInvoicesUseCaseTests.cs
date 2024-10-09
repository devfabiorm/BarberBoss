using BarberBoss.Application.UseCases.Invoices.GetAll;
using BarberBoss.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Invoices.GetAll;
public class GetAllInvoicesUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var user = UserBuilder.Build();
        var barberShop = BarberShopBuilder.Build();
        var invoices = InvoiceBuilder.Collection(user, barberShop);

        var useCase = CreateUseCase(invoices, user);

        //Act
        var result = await useCase.Execute();

        //Assert
        result.Should().NotBeNull();
        result.Invoices.Should().NotBeEmpty();
        result.Invoices.Should().HaveCount(2);
    }

    [Fact]
    public async Task Success_Empty()
    {
        //Arrange
        var user = UserBuilder.Build();
        var barberShop = BarberShopBuilder.Build();
        var invoices = new List<Invoice>();

        var useCase = CreateUseCase(invoices, user);

        //Act
        var result = await useCase.Execute();

        //Assert
        result.Should().NotBeNull();
        result.Invoices.Should().BeEmpty();
    }

    private GetAllInvoicesUseCase CreateUseCase(List<Invoice> invoices, User user)
    {
        var repository = new ReadOnlyInvoiceRepositoryBuilder().GetAll(user, invoices).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);


        return new GetAllInvoicesUseCase(repository, mapper, loggedUser);
    }
}
