using System.Net;
using FluentAssertions;
using Xunit;

namespace Greenfolio.API.FunctionalTests;

public class HealthEndpointTests : IClassFixture<WebAppFixture>
{
  private readonly WebAppFixture _fixture;

  public HealthEndpointTests(WebAppFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task Health_endpoint_returns_ok()
  {
    var client = _fixture.CreateClient();

    var response = await client.GetAsync("/health");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    (await response.Content.ReadAsStringAsync()).Should().Contain("\"status\":\"ok\"");
  }
}
