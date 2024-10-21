using BarberBoss.Exception.Messages;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Invoices.GetById;
public class GetInvoiceByIdTests : BarberBossClassFixture
{
    private const string MEHTOD = "api/Invoices";

    private readonly string _token;
    private readonly long _id;

    public GetInvoiceByIdTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_TeamMember.GetToken();
        _id = webApplicationFactory.Invoice.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGetAsync($"{MEHTOD}/{_id}", token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("title").GetString().Should().NotBeNullOrWhiteSpace();
        responseBody.RootElement.GetProperty("amount").GetDecimal().Should().BeGreaterThan(0);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_invoice_Not_Found(string language)
    {
        var response = await DoGetAsync($"{MEHTOD}/1000", token: _token, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();
        var exptectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVOICE_NOT_FOUND", new System.Globalization.CultureInfo(language));
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(exptectedMessage));
    }
}
