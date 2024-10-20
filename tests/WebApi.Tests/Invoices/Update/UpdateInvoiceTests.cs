using BarberBoss.Exception.Messages;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Invoices.Update;
public class UpdateInvoiceTests : BarberBossClassFixture
{
    private const string METHOD = "api/Invoices";

    private readonly string _token;
    private readonly long _invoiceId;

    public UpdateInvoiceTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_TeamMember.GetToken();
        _invoiceId = webApplicationFactory.Invoice.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestInvoiceJsonBuilder.Build();

        var response = await DoPutAsync($"{METHOD}/{_invoiceId}", request, token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string language)
    {
        var request = RequestInvoiceJsonBuilder.Build();
        request.Title = string.Empty;

        var response = await DoPutAsync($"{METHOD}/{_invoiceId}", request, token: _token, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("REQUIRED_TITLE", new CultureInfo(language));

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
