using BarberBoss.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UpdateOnlyUserRepositoryBuilder
{
    public static IUpdateOnlyUserRepository Build()
    {
        var mock = new Mock<IUpdateOnlyUserRepository>();

        return mock.Object;
    }
}
