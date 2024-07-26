using BarberBoss.Application.UseCases.Invoices.Report.Pdf.Fonts;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Invoices;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
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

        GlobalFontSettings.FontResolver = new InvoiceReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _repository.FilterByMonth(date);

        if (expenses.Count == 0) 
        {
            return [];
        }

        var document = CreateDocument(date);
        var page = CreatePage(document);
    }

    private Document CreateDocument(DateOnly date)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessages.INVOICES_FOR} {date:y}";

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }
}
