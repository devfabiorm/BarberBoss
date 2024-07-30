using BarberBoss.Application.UseCases.Invoices.Report.Pdf.Fonts;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Invoices;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Invoices.Report.Pdf;
public class GenerateInvoiceReportPdfUseCase : IGenerateInvoiceReportPdfUseCase
{
    private const string CURRENT_CURRENCY = "R$";

    private readonly IReadOnlyInvoiceRepository _repository;

    public GenerateInvoiceReportPdfUseCase(IReadOnlyInvoiceRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new InvoiceReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var invoices = await _repository.FilterByMonth(date);

        if (invoices.Count == 0) 
        {
            return [];
        }

        var document = CreateDocument(date);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        var totalInvoices = invoices
            .Where(invoice => invoice.Date.DayOfWeek >= DayOfWeek.Sunday && invoice.Date.DayOfWeek <= DayOfWeek.Saturday)
            .Sum(invoice => invoice.Amount);
        CreateTotalProfitSection(page, totalInvoices);

        return [];
    }

    private void CreateTotalProfitSection(Section page, decimal totalInvoices)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = paragraph.Format.SpaceAfter = 40;

        paragraph.AddFormattedText(ResourceReportGenerationMessages.PROFIT_OF_THE_WEEK);

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CURRENT_CURRENCY} {totalInvoices}", new Font { Name = FontHelper.BEBAS_NEUE_REGULAR });
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var filePath = Path.Combine(directoryName!, "UseCases", "Invoices", "Report", "Pdf", "Logo", "BarberBossaLogo.png");

        row.Cells[0].AddImage(filePath);

        row.Cells[1].AddParagraph("BARBEARIA DO JOÃO");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 25 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
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
