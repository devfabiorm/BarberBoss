using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.BarberShops.GetAll;
public class GetAllBarberShopsTests : BarberBossClassFixture
{
    private const string METHOD = "api/BarberShops";
    private readonly string _teamMemberToken;
    public GetAllBarberShopsTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _teamMemberToken = webApplicationFactory.User_TeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGetAsync(requestUri: METHOD, token: _teamMemberToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("barberShops").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
