using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Invoices.Report.Pdf.Fonts;
public class InvoiceReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        stream ??= ReadFontFile(FontHelper.DAFAULT_FONT);

        var length = (int)stream!.Length;

        var data = new byte[length];

        stream.Read(buffer: data, offset: 0, count: length);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        throw new NotImplementedException();
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"BarberBoss.Application.UseCases.Invoices.Report.Pdf.Fonts.{faceName}.ttf");
    }
}
