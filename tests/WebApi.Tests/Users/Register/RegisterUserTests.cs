using BarberBoss.Exception.Messages;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Users.Register;
public class RegisterUserTests : BarberBossClassFixture
{
    private const string METHOD = "api/Users";

    public RegisterUserTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await DoPostAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseData = await result.Content.ReadAsStreamAsync();

        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
        responseBody.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string language)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await DoPostAsync(METHOD, request, language: language);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await result.Content.ReadAsStreamAsync();

        var responseBody = await JsonDocument.ParseAsync(responseData);

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(language));

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
