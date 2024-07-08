namespace BarberBoss.Communication.Responses;
public class ResponseInvoiceShortJson
{
    public required string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
