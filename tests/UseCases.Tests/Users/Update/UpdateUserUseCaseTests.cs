using BarberBoss.Application.UseCases.Users.Update;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace UseCases.Tests.Users.Update;
public class UpdateUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var request = RequestUpdateUserJsonBuilder.Build();
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Name_Empty()
    {
        //Arrange
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.NAME_REQUIRED));
    }

    [Fact]
    public async Task Email_Empty()
    {
        //Arrange
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Email = string.Empty;
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.EMAIL_EMPTY));
    }

    [Fact]
    public async Task Email_Invalid()
    {
        //Arrange
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Email = "abc.ass";
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        //Act
        var act = async () => await useCase.Execute(request);

        //Assert
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();
        result.Where(ex => ex.GetErrorMessages().Count == 1 && ex.GetErrorMessages().Contains(ResourceErrorMessages.INVALID_EMAIL));
    }

    private UpdateUserUseCase CreateUseCase(User user)
    {
        var readOnlyRepository = new ReadOnlyUserRepositoryBuilder().GetByEmail(user.Email).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = UpdateOnlyUserRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build(); 

        return new UpdateUserUseCase(readOnlyRepository, loggedUser, repository, mapper, unitOfWork);
    }
}
