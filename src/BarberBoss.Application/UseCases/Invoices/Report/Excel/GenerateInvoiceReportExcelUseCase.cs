using BarberBoss.Domain.Repositories.Invoices;

namespace BarberBoss.Application.UseCases.Invoices.Report.Excel;
public class GenerateInvoiceReportExcelUseCase : IGenerateInvoiceReportExcelUseCase
{
    private readonly IReadOnlyInvoiceRepository _repository;

    public GenerateInvoiceReportExcelUseCase(IReadOnlyInvoiceRepository repository)
    {
        _repository = repository;
    }

    public Task<byte[]> Execute(DateOnly month)
    {
        throw new NotImplementedException();
    }
}
