namespace BarberBoss.Communication.Responses;
public class ResponseErrorJson
{
    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public List<string> ErrorMessages { get; }
}
