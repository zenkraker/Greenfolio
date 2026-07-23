using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Greenfolio.API.Infrastructure.Data;
using Greenfolio.API.Infrastructure.Email;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Greenfolio.API.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("DefaultConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseNpgsql(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

    services.Configure<MailserverConfiguration>(config.GetSection("Mailserver"));

    // Runs in-process: dashboard + background job server both live in the ASP.NET Core
    // process against Postgres storage. There is no standalone Hangfire container.
    services.AddHangfire(cfg => cfg.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));
    services.AddHangfireServer();

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
