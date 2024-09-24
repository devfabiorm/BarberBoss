using BarberBoss.Exception.ExceptionBase;
using BarberBoss.Exception.Messages;
using System.Net;

namespace BarberBoss.Exception;
public class InvalidLoginException : BarberBossException
{
    public InvalidLoginException() : base(ResourceErrorMessages.INVALID_CREDENTIALS)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }
}

