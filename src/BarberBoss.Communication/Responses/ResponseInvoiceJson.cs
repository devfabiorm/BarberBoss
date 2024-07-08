using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses;
public class ResponseInvoiceJson
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public EPaymentType PaymentType { get; set; }
    public DateTime Date { get; set; }
}
