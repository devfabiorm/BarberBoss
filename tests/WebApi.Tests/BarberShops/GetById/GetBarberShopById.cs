using BarberBoss.Exception.Messages;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.BarberShops.GetById;
public class GetBarberShopById : BarberBossClassFixture
{
    private const string METHOD = "api/BarberShops";

    private readonly string _teamMemberToken;
    private readonly long _shopId;
    private readonly string _shopName;

    public GetBarberShopById(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _teamMemberToken = webApplicationFactory.User_TeamMember.GetToken();
        _shopId = webApplicationFactory.Shop.GetId();
        _shopName = webApplicationFactory.Shop.GetName();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGetAsync($"{METHOD}/{_shopId}", token: _teamMemberToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("name").GetString().Should().Be(_shopName);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_Shop_Id(string language)
    {
        var response = await DoGetAsync($"{METHOD}/1000", token: _teamMemberToken, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_SHOP_ID", new CultureInfo(language));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
