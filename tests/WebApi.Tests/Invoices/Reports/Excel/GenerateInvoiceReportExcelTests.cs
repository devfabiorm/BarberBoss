using FluentAssertions;
using System.Net.Mime;
using System.Net;

namespace WebApi.Tests.Invoices.Reports.Excel;
public class GenerateInvoiceReportExcelTests : BarberBossClassFixture
{
    private const string METHOD = "api/Reports";

    private readonly string _adminToken;
    private readonly DateTime _invoiceDate;
    private readonly string _teamMemberToken;
    private readonly long _shoopId;

    public GenerateInvoiceReportExcelTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _adminToken = webApplicationFactory.User_Admin.GetToken();
        _teamMemberToken = webApplicationFactory.User_TeamMember.GetToken();
        _invoiceDate = webApplicationFactory.Invoice_Admin.GetDate();
        _shoopId = webApplicationFactory.Shop.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGetAsync(requestUri: $"{METHOD}/excel?week={_invoiceDate:d}&shopId={_shoopId}", token: _adminToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Octet);
    }

    [Fact]
    public async Task Error_Forbidden()
    {
        var response = await DoGetAsync(requestUri: $"{METHOD}/excel?week={_invoiceDate:d}&shopId={_shoopId}", token: _teamMemberToken);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
