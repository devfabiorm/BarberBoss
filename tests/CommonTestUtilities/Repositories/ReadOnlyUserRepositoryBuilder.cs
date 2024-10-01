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

    public ReadOnlyUserRepositoryBuilder GetByEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            _userRepositoryMock.Setup(r => r.HasActiveEmail(email)).ReturnsAsync(true);
        }

        return this;
    }

    public IReadOnlyUserRepository Build() => _userRepositoryMock.Object;
}
