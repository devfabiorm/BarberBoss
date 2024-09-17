using BarberBoss.Application.UseCases.BarberShops.Register;
using BarberBoss.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BarberShopsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterBarberShopUseCase useCase,
        RequestRegisterBarberShopJson request)
    {
        return Ok();
    }
}
