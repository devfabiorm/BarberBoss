using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApi.Tests.Users.Profile;
public class GetUserProfileTests : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/Users";
    private readonly string _name;
    private readonly string _token;
    private readonly string _email;

    private readonly HttpClient _httpClient;

    public GetUserProfileTests(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _name = webApplicationFactory.UserName;
        _email = webApplicationFactory.UserEmail;
        _token = webApplicationFactory.UserToken;
    }

    [Fact]
    public async Task Success()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var response = await _httpClient.GetAsync(METHOD);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("name").GetString().Should().Be(_name);
        responseBody.RootElement.GetProperty("email").GetString().Should().Be(_email);
    }
}
