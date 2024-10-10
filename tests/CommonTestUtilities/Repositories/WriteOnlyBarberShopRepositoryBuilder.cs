using BarberBoss.Domain.Repositories.BarberShops;
using Moq;

namespace CommonTestUtilities.Repositories;
public class WriteOnlyBarberShopRepositoryBuilder
{
    public static IWriteOnlyBarberShopRepository Build()
    {
        var mock = new Mock<IWriteOnlyBarberShopRepository>();

        return mock.Object;
    }
}
