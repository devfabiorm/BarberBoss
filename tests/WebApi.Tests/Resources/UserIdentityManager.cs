using BarberBoss.Domain.Entities;

namespace WebApi.Tests.Resources;
public class UserIdentityManager
{
    private readonly string _token;
    private readonly User _user;
    private readonly string _password;

    public UserIdentityManager(User user, string password, string token)
    {
        _token = token;
        _user = user;
        _password = password;
    }

    public string GetToken() => _token;

    public long GetId() => _user.Id;

    public string GetName() => _user.Name;

    public string GetEmail() => _user.Email;

    public string GetPassword() => _password;
}
