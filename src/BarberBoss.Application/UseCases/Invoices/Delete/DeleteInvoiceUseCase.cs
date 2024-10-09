using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoices;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;

namespace BarberBoss.Application.UseCases.Invoices.Delete;
public class DeleteInvoiceUseCase : IDeleteInvoiceUseCase
{
    private readonly IWriteOnlyInvoiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteInvoiceUseCase(
        IWriteOnlyInvoiceRepository repository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var result =  await _repository.Delete(id, loggedUser);

        if (result == false)
        {
            throw new NotFoundException(ResourceErrorMessages.INVOICE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
