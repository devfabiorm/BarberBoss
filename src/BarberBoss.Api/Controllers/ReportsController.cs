using BarberBoss.Application.UseCases.Invoices.Report.Excel;
using BarberBoss.Application.UseCases.Invoices.Report.Pdf;
using BarberBoss.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class ReportsController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateInvoiceReportExcelUseCase useCase,
        [FromQuery] DateOnly week, 
        [FromQuery] long shopId)
    {
        byte[] file = await useCase.Execute(week, shopId);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
        [FromServices] IGenerateInvoiceReportPdfUseCase useCase,
        [FromQuery] DateOnly week,
        [FromQuery] long shopId)
    {
        byte[] file = await useCase.Execute(week, shopId);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}
