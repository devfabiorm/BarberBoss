using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;

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
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
