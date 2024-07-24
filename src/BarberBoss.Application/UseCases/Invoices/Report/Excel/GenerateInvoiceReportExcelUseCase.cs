using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Invoices;
using ClosedXML.Excel;

namespace BarberBoss.Application.UseCases.Invoices.Report.Excel;
public class GenerateInvoiceReportExcelUseCase : IGenerateInvoiceReportExcelUseCase
{
    private const string CURRENT_CURRENCY = "$";
    private readonly IReadOnlyInvoiceRepository _repository;

    public GenerateInvoiceReportExcelUseCase(IReadOnlyInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var invoices = await _repository.FilterByMonth(month);

        if (invoices.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();

        workbook.Author = "Fabio Ribeiro";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var row = 2;

        foreach (var invoice in invoices) 
        { 
            worksheet.Cell("A1").Value = invoice.Title;
            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            worksheet.Cell("B1").Value = invoice.Date;
            worksheet.Cell("C1").Value = invoice.PaymentType.PaymentTypeToString();

            worksheet.Cell("D1").Value = invoice.Amount;
            worksheet.Cell("D1").Style.NumberFormat.Format = $"{CURRENT_CURRENCY} #,##0.00";
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            worksheet.Cell("E1").Value = invoice.Description;

            row++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private static void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#2e5759");
        worksheet.Cells("A1:E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

       
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
