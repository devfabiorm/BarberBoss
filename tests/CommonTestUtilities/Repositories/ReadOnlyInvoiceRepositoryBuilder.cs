﻿using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ReadOnlyInvoiceRepositoryBuilder
{
    private readonly Mock<IReadOnlyInvoiceRepository> _repository;

    public ReadOnlyInvoiceRepositoryBuilder()
    {
          _repository = new Mock<IReadOnlyInvoiceRepository>();
    }

    public ReadOnlyInvoiceRepositoryBuilder GetAll(List<Invoice> invoices)
    {
        _repository.Setup(repo => repo.GetAll()).ReturnsAsync(invoices);

        return this;
    }

    public IReadOnlyInvoiceRepository Build() =>_repository.Object;
}
