using FluentAssertions;
using System.Net;

namespace WebApi.Tests.BarberShops.Delete;
public class DeleteBarberShopTests : BarberBossClassFixture
{
    private const string METHOD = "api/BarberShops";

    private readonly string _adminUserToken;
    private readonly long _shopId;

    public DeleteBarberShopTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _adminUserToken = webApplicationFactory.User_Admin.GetToken();
        _shopId = webApplicationFactory.Shop.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoDeleteAsync(requestUri: $"{METHOD}/{_shopId}", token: _adminUserToken);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        response = await DoGetAsync(requestUri: $"{METHOD}/{_shopId}", token: _adminUserToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
