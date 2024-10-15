using BarberBoss.Application.UseCases.Login.DoLogin;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Tests.Login.DoLogin;
public class DoLoginUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user, request.Password);

        //Act
        var response = await useCase.Execute(request);

        //Assert
        response.Should().NotBeNull();
        response.Email.Should().Be(request.Email);
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Invalid_Login_Email()
    {
        //Arrange
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();


        var useCase = CreateUseCase(user, request.Password);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_CREDENTIALS));
    }


    [Fact]
    public async Task Error_Invalid_Login_Password()
    {
        //Arrange
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<InvalidLoginException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_CREDENTIALS));
    }

    private DoLoginUseCase CreateUseCase(User user, string? password = null)
    {
        var readOnlyRepository = new ReadOnlyUserRepositoryBuilder()
            .GetByEmail(user)
            .Build();
        var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();
        var accessGenerator = JwtTokenGeneratorBuilder.Build();

        return new DoLoginUseCase(readOnlyRepository, passwordEncrypter, accessGenerator);
    }
}
