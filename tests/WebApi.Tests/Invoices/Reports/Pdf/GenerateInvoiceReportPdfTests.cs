using FluentAssertions;
using System.Net;
using System.Net.Mime;

namespace WebApi.Tests.Invoices.Reports.Pdf;
public class GenerateInvoiceReportPdfTests : BarberBossClassFixture
{
    private const string METHOD = "api/Reports";

    private readonly string _adminToken;
    private readonly DateTime _invoiceDate;
    private readonly string _teamMemberToken;
    private readonly long _shopId;

    public GenerateInvoiceReportPdfTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _adminToken = webApplicationFactory.User_Admin.GetToken();
        _teamMemberToken = webApplicationFactory.User_TeamMember.GetToken();
        _invoiceDate = webApplicationFactory.Invoice_Admin.GetDate();
        _shopId = webApplicationFactory.Shop.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGetAsync(requestUri: $"{METHOD}/pdf?week={_invoiceDate:d}&shopId={_shopId}", token: _adminToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Pdf);
    }

    [Fact]
    public async Task Error_Forbidden()
    {
        var response = await DoGetAsync(requestUri: $"{METHOD}/pdf?week={_invoiceDate:d}&shopId={_shopId}", token: _teamMemberToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
