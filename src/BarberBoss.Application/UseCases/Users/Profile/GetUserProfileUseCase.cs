using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.Users.Profile;
public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetUserProfileUseCase(
        ILoggedUser loggedUser,
        IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUserJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        return _mapper.Map<ResponseUserJson>(loggedUser);
    }
}
