namespace BarberBoss.Application.UseCases.Invoices.Delete;
public interface IDeleteInvoiceUseCase
{
    Task Execute(long id);
}
