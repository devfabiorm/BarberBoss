namespace BarberBoss.Application.UseCases.Invoices.Report.Excel;
public interface IGenerateInvoiceReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly week, long shopId);
}
