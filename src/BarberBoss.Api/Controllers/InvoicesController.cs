using BarberBoss.Application.UseCases.Invoices.Delete;
using BarberBoss.Application.UseCases.Invoices.GetAll;
using BarberBoss.Application.UseCases.Invoices.GetById;
using BarberBoss.Application.UseCases.Invoices.Register;
using BarberBoss.Application.UseCases.Invoices.Update;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvoicesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterInvoiceUseCase useCase, [FromBody] RequestInvoiceJson request)
    {
        var response = await useCase.Execute(request);

        return CreatedAtAction(nameof(GetById), new { Id = response.Id }, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseInvoicesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllInvoicesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Invoices.Count == 0)
        {
            return NoContent();
        }

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseInvoiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetInvoiceByIdUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromServices] IInvoiceUpdateUseCase useCase, [FromRoute] long id, [FromBody] RequestInvoiceJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteInvoiceUseCase useCase, [FromRoute] long id) 
    {
        await useCase.Execute(id);

        return NoContent();
    }

}
