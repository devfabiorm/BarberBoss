using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;

namespace WebApi.Tests.Users.Delete;
public class DeleteUserAccountTests : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/Users";

    private readonly HttpClient _httpClient;

    private readonly string _token;

    public DeleteUserAccountTests(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _token = webApplicationFactory.UserToken;
    }

    [Fact]
    public async Task Success()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.DeleteAsync(METHOD);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
