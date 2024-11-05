namespace BarberBoss.Communication.Responses;
public class ResponseInvoicesJson
{
    public IList<ResponseInvoiceShortJson> Invoices { get; set; } = [];
}
