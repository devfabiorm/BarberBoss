using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class ReadOnlyUserRepositoryBuilder
{
    private readonly Mock<IReadOnlyUserRepository> _userRepositoryMock;

    public ReadOnlyUserRepositoryBuilder()
    {
        _userRepositoryMock = new Mock<IReadOnlyUserRepository>();
    }

    public ReadOnlyUserRepositoryBuilder HasActiveEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            _userRepositoryMock.Setup(r => r.HasActiveEmail(email)).ReturnsAsync(true);
        }

        return this;
    }

    public ReadOnlyUserRepositoryBuilder GetByEmail(User user)
    {
        if (user is not null)
        {
            _userRepositoryMock.Setup(r => r.GetByEmail(user.Email)).ReturnsAsync(user);
        }

        return this;
    }

    public IReadOnlyUserRepository Build() => _userRepositoryMock.Object;
}
