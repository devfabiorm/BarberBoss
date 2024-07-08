using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Requests;
public class RequestRegisterInvoiceJson
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public EPaymentType PaymentType { get; set; }
    public DateTime Date { get; set; }
}
