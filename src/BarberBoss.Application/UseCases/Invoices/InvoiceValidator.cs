using BarberBoss.Communication.Requests;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Invoices;
public class InvoiceValidator : AbstractValidator<RequestInvoiceJson>
{
    public InvoiceValidator()
    {
        RuleFor(invoice => invoice.Title)
            .NotEmpty()
            .WithMessage("Title is a required field.");

        RuleFor(invoice => invoice.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("The invoice needs to be of a older date than today.");

        RuleFor(invoice => invoice.Amount)
            .GreaterThan(0)
            .WithMessage("The invoice value amount needs to be greater than zero.");

        RuleFor(invoice => invoice.PaymentType)
            .IsInEnum()
            .WithMessage("The payment type needs to be a valid one.");

    }
}
