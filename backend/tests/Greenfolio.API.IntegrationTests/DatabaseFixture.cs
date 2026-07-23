using Greenfolio.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace Greenfolio.API.IntegrationTests;

// Spins up a real, ephemeral Postgres 16 container per test run - the same engine as
// production, not an in-memory substitute. Requires Docker.
public class DatabaseFixture : IAsyncLifetime
{
  private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
    .WithImage("postgres:16")
    .WithDatabase("greenfolio")
    .WithUsername("devuser")
    .WithPassword("devpassword")
    .Build();

  public AppDbContext CreateContext()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseNpgsql(_container.GetConnectionString())
      .Options;
    return new AppDbContext(options, null);
  }

  public async Task InitializeAsync()
  {
    await _container.StartAsync();
    using var context = CreateContext();
    await context.Database.MigrateAsync();
  }

  public async Task DisposeAsync() => await _container.DisposeAsync();
}
