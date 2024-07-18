using System.Diagnostics;

namespace BarberBoss.Exception.ExceptionBase;
public abstract class BarberBossException : SystemException
{
    protected BarberBossException(string message) : base(message)
    {

    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrorMessages();
}
