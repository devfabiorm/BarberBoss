using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Tests;
public class BarberBossClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public BarberBossClassFixture(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    protected async Task<HttpResponseMessage> DoGetAsync(string requestUri, string? token = null, string? language = null)
    {
        SetHeaders(token, language);

        return await _httpClient.GetAsync(requestUri);
    }

    protected async Task<HttpResponseMessage> DoPostAsync(string requestUri, object requestBody, string? token = null, string? language = null)
    {
        SetHeaders(token, language);

        return await _httpClient.PostAsJsonAsync(requestUri, requestBody);
    }

    protected async Task<HttpResponseMessage> DoPutAsync(string requestUri, object requestBody, string? token = null, string? language = null)
    {
        SetHeaders(token, language);

        return await _httpClient.PutAsJsonAsync(requestUri, requestBody);
    }

    protected async Task<HttpResponseMessage> DoDeleteAsync(string requestUri, string? token = null, string? language = null)
    {
        SetHeaders(token, language);

        return await _httpClient.DeleteAsync(requestUri);
    }

    private void SetHeaders(string? token = null, string? language = null)
    {
        if (token is not null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (language is not null) 
        { 
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
        }
    }
}
 