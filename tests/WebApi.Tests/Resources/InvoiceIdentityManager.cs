using BarberBoss.Domain.Entities;

namespace WebApi.Tests.Resources;
public class InvoiceIdentityManager
{
    private readonly Invoice _invoice;

    public InvoiceIdentityManager(Invoice invoice)
    {
        _invoice = invoice;
    }

    public long GetId() => _invoice.Id;
}
