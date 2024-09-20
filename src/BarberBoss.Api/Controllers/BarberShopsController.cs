using BarberBoss.Application.UseCases.BarberShops.GetAll;
using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BarberShopsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseBarberShopJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterBarberShopUseCase useCase,
        [FromBody] RequestRegisterBarberShopJson request)
    {
        var barberShop = await useCase.Execute(request);

        return Ok(barberShop);
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
}
