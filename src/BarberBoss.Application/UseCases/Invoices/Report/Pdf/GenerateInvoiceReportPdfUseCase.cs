using BarberBoss.Application.UseCases.Invoices.Report.Pdf.Fonts;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Invoices;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;
using MigraDoc.DocumentObjectModel.Tables;
using BarberBoss.Domain.Extensions;
using BarberBoss.Application.UseCases.Invoices.Report.Pdf.Colors;
using MigraDoc.Rendering;

namespace BarberBoss.Application.UseCases.Invoices.Report.Pdf;
public class GenerateInvoiceReportPdfUseCase : IGenerateInvoiceReportPdfUseCase
{
    private const string CURRENT_CURRENCY = "R$";
    private const int HEIGHT_ROW_EXPENSE_TABLE = 25;
    private readonly IReadOnlyInvoiceRepository _repository;

    public GenerateInvoiceReportPdfUseCase(IReadOnlyInvoiceRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new InvoiceReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var invoices = await _repository.FilterByWeek(date);

        if (invoices.Count == 0) 
        {
            return [];
        }

        var document = CreateDocument(date);
        var style = document.AddStyle("DataCell", "Normal");
        style.ParagraphFormat.Borders.Distance = 8;

        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        var totalInvoices = invoices
            .Sum(invoice => invoice.Amount);
        CreateTotalProfitSection(page, totalInvoices);

        foreach (var invoice in invoices) 
        {
            var table = CreateInvoiceTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            AddInvoiceTitle(row.Cells[0], invoice.Title);
            AddHeaderForAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSE_TABLE;

            row.Cells[0].AddParagraph(invoice.Date.ToString("D"));
            SetBaseStyleForInvoiceInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 9;

            row.Cells[1].AddParagraph(invoice.Date.ToString("t"));
            SetBaseStyleForInvoiceInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(invoice.PaymentType.PaymentTypeToString());
            SetBaseStyleForInvoiceInformation(row.Cells[2]);

            AddAmountForExpense(row.Cells[3], invoice.Amount);

            if(!string.IsNullOrWhiteSpace(invoice.Description))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_EXPENSE_TABLE;

                descriptionRow.Cells[0].AddParagraph(invoice.Description);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 9, Color = ColorsHelper.LIGHT_GRAY };
                descriptionRow.Cells[0].Shading.Color = ColorsHelper.AQUA_GREEN;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent= 9;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }

        return RenderDocument(document);
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private void AddAmountForExpense(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{CURRENT_CURRENCY} {amount}");
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Top;
    }

    private void SetBaseStyleForInvoiceInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.LIGHT_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.BEBAS_NEUE_REGULAR, Size = 15, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.DARK_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddInvoiceTitle(Cell cell, string invoiceTitle)
    {
        cell.AddParagraph(invoiceTitle);
        cell.Format.Font = new Font { Name = FontHelper.BEBAS_NEUE_REGULAR, Size = 15, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.DARKENED_DARK_GREEN;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 9;
    }

    private void CreateTotalProfitSection(Section page, decimal totalInvoices)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = paragraph.Format.SpaceAfter = 40;

        paragraph.AddFormattedText(ResourceReportGenerationMessages.PROFIT_OF_THE_WEEK, new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 15 });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CURRENT_CURRENCY} {totalInvoices}", new Font { Name = FontHelper.BEBAS_NEUE_REGULAR, Size = 50 });
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("230");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var filePath = Path.Combine(directoryName!, "UseCases", "Invoices", "Report", "Pdf", "Logo", "BarberBossaLogo.png");

        row.Cells[0].AddImage(filePath);

        row.Cells[1].AddParagraph("BARBEARIA DO JOÃO");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 25 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private Table CreateInvoiceTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("154").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("124").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("153").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("69").Format.Alignment = ParagraphAlignment.Right;

        return table;
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
