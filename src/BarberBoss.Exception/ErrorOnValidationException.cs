using BarberBoss.Exception.ExceptionBase;
using System.Net;

namespace BarberBoss.Exception;
public class ErrorOnValidationException : BarberBossException
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }

    public override List<string> GetErrorMessages()
    {
        return _errors;
    }
}
