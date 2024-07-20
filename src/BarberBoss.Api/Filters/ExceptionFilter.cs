using BarberBoss.Communication.Responses;
using BarberBoss.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBoss.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is BarberBossException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownException(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var barberBossException = (BarberBossException)context.Exception;
        var errorResponse = new ResponseErrorJson(barberBossException.GetErrorMessages());

        context.HttpContext.Response.StatusCode = barberBossException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnknownException(ExceptionContext context) 
    {
        var errorResponse = new ResponseErrorJson("Unknown Error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
