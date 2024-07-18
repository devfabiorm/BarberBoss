using BarberBoss.Exception.ExceptionBase;
using System.Net;

namespace BarberBoss.Exception;
public class NotFoundException : BarberBossException
{
    public NotFoundException(string errorMessage) : base(errorMessage)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }
}
