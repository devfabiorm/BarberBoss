using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;

namespace BarberBoss.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this EPaymentType paymentType)
    {
        return paymentType switch
        {
            EPaymentType.Cash => ResourceReportGenerationMessages.CASH,
            EPaymentType.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
            EPaymentType.CreditCard => ResourceReportGenerationMessages.CREDIT_CARD,
            EPaymentType.Pix => ResourceReportGenerationMessages.PIX,
            EPaymentType.EletronicTransfer => ResourceReportGenerationMessages.ELETRONIC_TRANSFER,
            _ => string.Empty,
        };
    }
}
