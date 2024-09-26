using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseUserJson> Execute(RequestRegisterUserJson request);
}
