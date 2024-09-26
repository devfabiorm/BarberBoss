using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.Users.Delete;
public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUpdateOnlyUserRepository _updateRepository;
    private readonly IWriteOnlyUserRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(
        ILoggedUser loggedUser,
        IUpdateOnlyUserRepository updateRepository,
        IWriteOnlyUserRepository writeRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _updateRepository = updateRepository;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var user = await _updateRepository.GetById(loggedUser.Id);

        _writeRepository.Delete(user);

        await _unitOfWork.Commit();
    }
}
