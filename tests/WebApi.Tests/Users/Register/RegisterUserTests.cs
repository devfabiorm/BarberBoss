using BarberBoss.Exception.Messages;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.Users.Register;
public class RegisterUserTests : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/Users";

    private readonly HttpClient _httpClient;

    public RegisterUserTests(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseData = await result.Content.ReadAsStreamAsync();

        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
        responseBody.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("fr")]
    [InlineData("pt-BR")]
    [InlineData("pt-PT")]
    [InlineData("en")]
    public async Task Error_Empty_Name(string language)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await result.Content.ReadAsStreamAsync();

        var responseBody = await JsonDocument.ParseAsync(responseData);

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(language));

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
