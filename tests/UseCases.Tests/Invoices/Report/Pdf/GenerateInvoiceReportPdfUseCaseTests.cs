using BarberBoss.Application.UseCases.Invoices.Report.Pdf;
using BarberBoss.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Invoices.Report.Pdf;
public class GenerateInvoiceReportPdfUseCaseTests
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

    private GenerateInvoiceReportPdfUseCase CreateUseCase(List<Invoice> invoices, User user)
    {
        var repository = new ReadOnlyInvoiceRepositoryBuilder()
            .FilterByWeek(user, invoices)
            .Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateInvoiceReportPdfUseCase(repository, loggedUser);
    }
}
