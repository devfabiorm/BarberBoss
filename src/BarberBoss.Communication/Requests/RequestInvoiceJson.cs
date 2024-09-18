using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Requests;
public class RequestInvoiceJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public EPaymentType PaymentType { get; set; }
    public DateTime Date { get; set; }
    public long BarberShopId { get; set; }
}
