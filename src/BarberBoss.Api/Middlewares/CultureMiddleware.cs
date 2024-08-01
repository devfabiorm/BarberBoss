using System.Globalization;

namespace BarberBoss.Api.Middlewares;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate neext)
    {
        _next = neext;
    }

    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        var requestedLanguage = context.Request.Headers.Accept.FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (!string.IsNullOrWhiteSpace(requestedLanguage)
            && supportedLanguages.Exists(language => language.Name.Equals(requestedLanguage)))
        {
            cultureInfo = new CultureInfo(requestedLanguage);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    } 
}
