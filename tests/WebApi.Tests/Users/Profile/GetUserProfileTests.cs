using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Users.Profile;
public class GetUserProfileTests : BarberBossClassFixture
{
    private const string METHOD = "api/Users";
    private readonly string _name;
    private readonly string _token;
    private readonly string _email;

    public GetUserProfileTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _name = webApplicationFactory.UserName;
        _email = webApplicationFactory.UserEmail;
        _token = webApplicationFactory.UserToken;
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGetAsync(METHOD, token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("name").GetString().Should().Be(_name);
        responseBody.RootElement.GetProperty("email").GetString().Should().Be(_email);
    }
}
