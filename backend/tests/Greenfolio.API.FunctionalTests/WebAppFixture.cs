using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;
using Xunit;

namespace Greenfolio.API.FunctionalTests;

// Boots the real app (Program.cs, including its startup migrate+seed) against an ephemeral
// Postgres container, so this exercises the actual wired-up pipeline, not a test double.
public class WebAppFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
  private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
    .WithImage("postgres:16")
    .WithDatabase("greenfolio")
    .WithUsername("devuser")
    .WithPassword("devpassword")
    .Build();

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureAppConfiguration((_, config) =>
    {
      config.AddInMemoryCollection(new Dictionary<string, string?>
      {
        ["ConnectionStrings:DefaultConnection"] = _container.GetConnectionString(),
      });
    });
  }

  public async Task InitializeAsync() => await _container.StartAsync();

  async Task IAsyncLifetime.DisposeAsync()
  {
    await _container.DisposeAsync();
    await base.DisposeAsync();
  }
}
