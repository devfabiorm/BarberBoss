using BarberBoss.Application.UseCases.Users.Delete;
using BarberBoss.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Tests.Users.Delete;
public class DeleteUserAccountUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        //Arrange
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser);

        //Act
        var act = useCase.Execute;

        //Assert
        await act.Should().NotThrowAsync();
    }

    private DeleteUserAccountUseCase CreateUseCase(User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var updateRepository = UpdateOnlyUserRepositoryBuilder.Build();
        var repository = WriteOnlyUserRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        return new DeleteUserAccountUseCase(loggedUser, updateRepository, repository, unitOfWork);
    }
}
