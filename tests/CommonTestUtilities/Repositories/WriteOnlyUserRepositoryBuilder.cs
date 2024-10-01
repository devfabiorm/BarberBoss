using BarberBoss.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class WriteOnlyUserRepositoryBuilder
{
    public static IWriteOnlyUserRepository Build()
    {
        var mock = new Mock<IWriteOnlyUserRepository>();
        return mock.Object;
    }
}
