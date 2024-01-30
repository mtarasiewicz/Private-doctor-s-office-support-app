using System.Security.Claims;
using DocHub.Core.Domain.Entities.IdentityEntities;
using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DocHub.IntegrationTests;

public class AppointmentsControllerIntegrationTest : IClassFixture<WebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _factory;

    public AppointmentsControllerIntegrationTest(WebApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Index_ShouldReturnView()
    {
    }
}