using BarberBoss.Application.UseCases.Invoices.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Register([FromServices] IRegisterInvoiceUseCase useCase, [FromBody] RequestRegisterInvoiceJson request)
    {
        var response = useCase.Execute(request);

        return CreatedAtAction(nameof(GetById), new { Id = response.Id }, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseInvoiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] long id)
    {
        return Ok();
    }
}
