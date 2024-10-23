using BarberBoss.Exception.Messages;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.BarberShops.Register;
public class RegisterBarberShopTests : BarberBossClassFixture
{
    private const string METHOD = "api/BarberShops";
    private readonly string _adminUserToken;

    public RegisterBarberShopTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _adminUserToken = webApplicationFactory.User_Admin.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();

        var response = await DoPostAsync(requestUri: METHOD, requestBody: request, token: _adminUserToken);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string language)
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPostAsync(requestUri: METHOD, requestBody: request, token: _adminUserToken, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_SHOP_NAME", new CultureInfo(language));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
