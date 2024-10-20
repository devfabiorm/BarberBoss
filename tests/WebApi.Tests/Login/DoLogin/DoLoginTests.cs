using BarberBoss.Exception.Messages;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Login.DoLogin;
public class DoLoginTests : BarberBossClassFixture
{

    private const string METHOD = "api/Login";
    private readonly string _userEmail;
    private readonly string _userPassword;

    public DoLoginTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _userEmail = webApplicationFactory.User_TeamMember.GetEmail();
        _userPassword = webApplicationFactory.User_TeamMember.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestLoginJsonBuilder.Build();
        request.Email = _userEmail;
        request.Password = _userPassword;

        var response = await DoPostAsync(METHOD, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        responseBody.RootElement.GetProperty("email").GetString().Should().Be(request.Email);
        responseBody.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_Login_Email(string language)
    {
        var request = RequestLoginJsonBuilder.Build();
        request.Password = _userPassword;

        var response = await DoPostAsync(METHOD, request, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_CREDENTIALS", new CultureInfo(language));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_Login_Password(string language)
    {
        var request = RequestLoginJsonBuilder.Build();
        request.Email = _userEmail;

        var response = await DoPostAsync(METHOD, request, language: language);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonDocument.ParseAsync(responseData);

        var errors = responseBody.RootElement.GetProperty("errorMessages").EnumerateArray();

        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("INVALID_CREDENTIALS", new CultureInfo(language));

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}
