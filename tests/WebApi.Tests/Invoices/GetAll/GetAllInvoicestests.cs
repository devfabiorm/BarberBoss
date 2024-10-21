using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Invoices.GetAll;
public class GetAllInvoicestests : BarberBossClassFixture
{
    private readonly string _token;
    private const string METHOD = "api/Invoices";
    public GetAllInvoicestests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_TeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGetAsync(METHOD, token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var invoices = responseBody.RootElement.GetProperty("invoices").EnumerateArray();

        invoices.Should().HaveCount(2);
    }
}
