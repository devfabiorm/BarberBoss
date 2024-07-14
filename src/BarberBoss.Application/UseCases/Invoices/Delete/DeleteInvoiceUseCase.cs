using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Invoices.Delete;
internal class DeleteInvoiceUseCase : IDeleteInvoiceUseCase
{
    private readonly IWriteOnlyInvoiceRepository _repository;

    public DeleteInvoiceUseCase(IWriteOnlyInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(long id)
    {
        var result =  await _repository.Delete(id);

        if (result == false)
        {
            //throw exception
        }
    }
}
