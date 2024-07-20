namespace BarberBoss.Communication.Responses;
public class ResponseErrorJson
{
    public ResponseErrorJson(List<string> errorMessages)
    {
        Errors = errorMessages;
    }

    public ResponseErrorJson(string errorMessage)
    {
        Errors = [errorMessage];
    }

    public List<string> Errors { get; }
}
