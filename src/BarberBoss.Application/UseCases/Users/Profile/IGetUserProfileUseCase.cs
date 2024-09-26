using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.Users.Profile;
public interface IGetUserProfileUseCase
{
    Task<ResponseUserJson> Execute();
}
