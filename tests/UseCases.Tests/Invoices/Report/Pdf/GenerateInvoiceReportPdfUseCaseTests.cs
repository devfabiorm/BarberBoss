﻿using BarberBoss.Application.UseCases.Invoices.Report.Pdf;
using BarberBoss.Domain.Entities;
using CommonTestUtilities.Entities;
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

        var useCase = CreateUseCase(invoices);

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
         var useCase = CreateUseCase([]);

        //Act
        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        //Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    private GenerateInvoiceReportPdfUseCase CreateUseCase(List<Invoice> invoices)
    {
        var repository = new ReadOnlyInvoiceRepositoryBuilder()
            .FilterByWeek(invoices)
            .Build();

        return new GenerateInvoiceReportPdfUseCase(repository);
    }
}
