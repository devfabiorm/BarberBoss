using BarberBoss.Domain.Constants;
using BarberBoss.Domain.Entities;
using Bogus;
using CommonTestUtilities.Cryptography;

namespace CommonTestUtilities.Entities;
public class UserBuilder
{
    public static User Build(string role = Roles.TeamMember)
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Build();

        return new Faker<User>()
            .RuleFor(user => user.Id, _ => 1)
            .RuleFor(user => user.Name, faker => faker.Name.FirstName())
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(user => user.Password, (_, user) => passwordEncrypter.Encrypt(user.Password))
            .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid())
            .RuleFor(user => user.Role, _ => role);
    }
}
