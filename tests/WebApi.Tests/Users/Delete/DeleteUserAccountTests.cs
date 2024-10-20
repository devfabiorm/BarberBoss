using FluentAssertions;
using System.Net;

namespace WebApi.Tests.Users.Delete;
public class DeleteUserAccountTests : BarberBossClassFixture
{
    private const string METHOD = "api/Users";

    private readonly string _token;

    public DeleteUserAccountTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_TeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoDeleteAsync(METHOD, token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
