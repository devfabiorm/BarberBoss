using BarberBoss.Exception.Messages;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Invoices;
public class RegisterInvoiceTests : BarberBossClassFixture
{
    private readonly string _token;
    private readonly long _shopId;

    private const string METHOD = "api/Invoices";

    public RegisterInvoiceTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.UserToken;
        _shopId = webApplicationFactory.ShopId;
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestInvoiceJsonBuilder.Build();
        request.BarberShopId = _shopId;

        var response = await DoPostAsync(METHOD, request, token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
        responseBody.RootElement.GetProperty("description").GetString().Should().Be(request.Description);
        responseBody.RootElement.GetProperty("amount").GetDecimal().Should().Be(request.Amount);
        responseBody.RootElement.GetProperty("date").GetDateTime().Should().Be(request.Date);
        responseBody.RootElement.GetProperty("paymentType").GetInt32().Should().Be((int)request.PaymentType);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_Shop(string language)
    {
        var request = RequestInvoiceJsonBuilder.Build();

        var response = await DoPostAsync(METHOD, request, token: _token, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_SHOP_ID", new CultureInfo(language));

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Title(string language)
    {
        var request = RequestInvoiceJsonBuilder.Build();
        request.BarberShopId = _shopId;
        request.Title = string.Empty;

        var response = await DoPostAsync(METHOD, request, token: _token, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("REQUIRED_TITLE", new CultureInfo(language));

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
