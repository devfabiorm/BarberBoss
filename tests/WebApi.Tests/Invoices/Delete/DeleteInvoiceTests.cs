using FluentAssertions;
using System.Net;

namespace WebApi.Tests.Invoices.Delete;
public class DeleteInvoiceTests : BarberBossClassFixture
{
    private const string METTHOD = "api/Invoices";
    private readonly long _id;
    private readonly string _token;

    public DeleteInvoiceTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.User_TeamMember.GetToken();
        _id = webApplicationFactory.Invoice_TeamMember.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoDeleteAsync($"{METTHOD}/{_id}", token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        response = await DoGetAsync($"{METTHOD}/{_id}", token: _token);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
