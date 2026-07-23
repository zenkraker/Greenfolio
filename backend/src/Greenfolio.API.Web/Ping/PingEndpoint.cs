using FastEndpoints;

namespace Greenfolio.API.Web.Ping;

// Minimal placeholder endpoint - FastEndpoints requires at least one declaration to start up.
// Real endpoints (search, org profiles, etc.) land starting M2.
public class PingEndpoint : EndpointWithoutRequest<string>
{
  public override void Configure()
  {
    Get("/ping");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    await SendAsync("pong", cancellation: ct);
  }
}
