using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// GDPR removal requests (brief §3.2.2 / §6 E5) — checked on every re-ingestion so a removed
// person/org doesn't silently reappear on the next CORDIS/OpenAlex import.
public class SuppressionList(SuppressionMatcher matcher, string value, string reason) : EntityBase, IAggregateRoot
{
  public SuppressionMatcher Matcher { get; private set; } = matcher;
  public string Value { get; private set; } = Guard.Against.NullOrEmpty(value, nameof(value));
  public string Reason { get; private set; } = Guard.Against.NullOrEmpty(reason, nameof(reason));
  public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}
