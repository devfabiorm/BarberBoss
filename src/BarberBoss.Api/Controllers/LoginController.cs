using BarberBoss.Application.UseCases.Login.DoLogin;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoggedJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DoLogin(
            [FromServices] IDoLoginUseCase useCase,
            [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
