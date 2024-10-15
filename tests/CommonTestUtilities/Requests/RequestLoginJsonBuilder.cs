using BarberBoss.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;
public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(login => login.Email, faker => faker.Internet.Email())
            .RuleFor(login => login.Password, faker => faker.Internet.Password());
    }
}
