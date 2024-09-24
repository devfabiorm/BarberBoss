using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception;

namespace BarberBoss.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IReadOnlyUserRepository _repository;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IReadOnlyUserRepository repository,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseLoggedJson> Execute(RequestLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        var isCorrectPassword = _passwordEncrypter.Verify(user.Password, request.Password);

        if (!isCorrectPassword)
        {
            throw new InvalidLoginException();
        }

        return new ResponseLoggedJson
        {
            Email = user.Email,
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}
