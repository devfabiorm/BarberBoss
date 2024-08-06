namespace BarberBoss.Communication.Responses;
public class ResponseInvoiceShortJson
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
