namespace BarberBoss.Application.UseCases.Invoices.Report.Pdf;
public interface IGenerateInvoiceReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly date);
}
