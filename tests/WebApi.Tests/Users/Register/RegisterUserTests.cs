﻿using CommonTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Tests.Users.Register;
public class RegisterUserTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const string METHOD = "api/Users";

    private readonly HttpClient _httpClient;

    public RegisterUserTests(WebApplicationFactory<Program> webApplication)
    {
        _httpClient = webApplication.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}