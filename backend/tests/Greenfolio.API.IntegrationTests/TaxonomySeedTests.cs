using FluentAssertions;
using Greenfolio.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Greenfolio.API.IntegrationTests;

public class TaxonomySeedTests : IClassFixture<DatabaseFixture>
{
  private readonly DatabaseFixture _fixture;

  public TaxonomySeedTests(DatabaseFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task Migration_creates_all_greengraph_tables()
  {
    using var context = _fixture.CreateContext();

    var tableNames = await context.Database
      .SqlQuery<string>($"SELECT table_name AS \"Value\" FROM information_schema.tables WHERE table_schema = 'public'")
      .ToListAsync();

    tableNames.Should().Contain([
      "organizations", "projects", "sources", "fields", "topics",
      "euro_sci_voc_crosswalk", "collab_edges", "claims", "users",
      "collab_requests", "scouting_leads", "events", "suppression_list"
    ]);
  }

  [Fact]
  public async Task SeedData_populates_sources_and_taxonomy_exactly_once()
  {
    using var context = _fixture.CreateContext();

    SeedData.SeedSources(context);
    TaxonomySeed.Seed(context);
    // Re-running must be a no-op, not a duplicate insert - this is what startup does on every restart.
    SeedData.SeedSources(context);
    TaxonomySeed.Seed(context);

    (await context.Sources.CountAsync()).Should().Be(3);
    (await context.Fields.CountAsync()).Should().Be(13);
    (await context.Topics.CountAsync()).Should().Be(62);
    (await context.EuroSciVocCrosswalks.CountAsync()).Should().BeGreaterThan(0);
  }
}
