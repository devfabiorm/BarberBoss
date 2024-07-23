using BarberBoss.Communication.Requests;
using BarberBoss.Exception.Messages;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Invoices;
public class InvoiceValidator : AbstractValidator<RequestInvoiceJson>
{
    public InvoiceValidator()
    {
        RuleFor(invoice => invoice.Title)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_TITLE);

        RuleFor(invoice => invoice.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(ResourceErrorMessages.INVOICE_DATE_NEEDS_OLDER_THAN_CURRENT);

        RuleFor(invoice => invoice.Amount)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.INVOICE_AMOUNT_GREATER_THAN_ZERO);

        RuleFor(invoice => invoice.PaymentType)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.INVALID_PAYMENT_TYPE);

    }
}
