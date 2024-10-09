using BarberBoss.Application.UseCases.Invoices.Report.Excel;
using BarberBoss.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Invoices.Report.Excel;
public class GenerateInvoiceReportExcelUseCaseTests
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
        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        //Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Empty_Return()
    {
        //Arrange
        var user = UserBuilder.Build();
        var useCase = CreateUseCase([], user);

        //Act
        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    public GenerateInvoiceReportExcelUseCase CreateUseCase(List<Invoice> invoices, User user)
    {
        var repository = new ReadOnlyInvoiceRepositoryBuilder()
            .FilterByWeek(user, invoices)
            .Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateInvoiceReportExcelUseCase(repository, loggedUser);
    }
}
