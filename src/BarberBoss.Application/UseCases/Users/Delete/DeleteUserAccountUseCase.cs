using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.Users.Delete;
public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUpdateOnlyUserRepository _updateRepository;
    private readonly IWriteOnlyUserRepository _writeRepository;

    public DeleteUserAccountUseCase(
        ILoggedUser loggedUser,
        IUpdateOnlyUserRepository updateRepository,
        IWriteOnlyUserRepository writeRepository)
    {
        _loggedUser = loggedUser;
        _updateRepository = updateRepository;
        _writeRepository = writeRepository;
    }

    public async Task Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var user = await _updateRepository.GetById(loggedUser.Id);

        _writeRepository.Delete(user);
    }
}
