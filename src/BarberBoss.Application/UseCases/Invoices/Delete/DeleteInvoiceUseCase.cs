using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoices;

namespace BarberBoss.Application.UseCases.Invoices.Delete;
internal class DeleteInvoiceUseCase : IDeleteInvoiceUseCase
{
    private readonly IWriteOnlyInvoiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInvoiceUseCase(IWriteOnlyInvoiceRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result =  await _repository.Delete(id);

        if (result == false)
        {
            //throw exception
        }

        await _unitOfWork.Commit();
    }
}
