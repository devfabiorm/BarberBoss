using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task Execute(RequestRegisterUserJson request);
}
