using BarberBoss.Application.UseCases.BarberShops.Delete;
using BarberBoss.Application.UseCases.BarberShops.GetAll;
using BarberBoss.Application.UseCases.BarberShops.GetById;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BarberShopsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseBarberShopJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterBarberShopUseCase useCase,
        [FromBody] RequestRegisterBarberShopJson request)
    {
        var barberShop = await useCase.Execute(request);

        return CreatedAtAction(nameof(GetById), new { barberShop.Id }, barberShop);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBarberShopJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] long id,
        [FromServices] IGetBarberShopByIdUseCase useCase)
    {
        var result = await useCase.Execute(id);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseBarberShopsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllBarberShopsUseCase useCase)
    {
        var barberShops = await useCase.Execute();

        if (barberShops.BarberShops.Count() == 0) 
        {
            return NoContent();
        }

        return Ok(barberShops);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] long id,
        [FromServices] IDeleteBarberShopUseCase useCase)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
