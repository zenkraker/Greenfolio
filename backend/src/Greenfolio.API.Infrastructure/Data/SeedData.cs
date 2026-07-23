using Greenfolio.API.Core.GreenGraph;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Greenfolio.API.Infrastructure.Data;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);

    SeedSources(dbContext);
    TaxonomySeed.Seed(dbContext);
  }

  public static void SeedSources(AppDbContext dbContext)
  {
    if (dbContext.Sources.Any()) return;

    // Brief §3.1 Tier-1 sources targeted for M1, seeded now so licence/attribution
    // tracking (§3.2.4) exists from the very first migration, not bolted on later.
    dbContext.Sources.AddRange(
      new Source("cordis", "CORDIS", "EU open data (attribution)", true, "https://cordis.europa.eu"),
      new Source("openalex", "OpenAlex", "CC0", false, "https://openalex.org"),
      new Source("ror", "ROR Registry", "CC0", false, "https://ror.org")
    );

    dbContext.SaveChanges();
  }
}
