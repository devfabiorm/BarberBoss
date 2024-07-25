using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Invoices;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBoss.Application.UseCases.Invoices.Report.Pdf;
public class GenerateInvoiceReportPdfUseCase : IGenerateInvoiceReportPdfUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;

    public GenerateInvoiceReportPdfUseCase(IReadOnlyInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _repository.FilterByMonth(date);

        if (expenses.Count == 0) 
        {
            return [];
        }

        var document = CreateDocument(date);
    }

    private Document CreateDocument(DateOnly date)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessages.INVOICES_FOR} {date:y}";

        return document;
    }
}
