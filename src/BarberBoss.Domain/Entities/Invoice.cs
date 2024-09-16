using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.Entities;
public class Invoice
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public EPaymentType PaymentType { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
