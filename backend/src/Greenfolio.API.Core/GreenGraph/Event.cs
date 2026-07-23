using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// First-party analytics event, cookieless (brief §3.2.6) — no cross-session identifiers.
public class Event(EventType type) : EntityBase, IAggregateRoot
{
  public DateTimeOffset OccurredAt { get; private set; } = DateTimeOffset.UtcNow;
  public EventType Type { get; private set; } = type;
  public int? OrgId { get; set; }
  public int? TopicId { get; set; }
  public string? QueryText { get; set; }
  public string? MetaJson { get; set; }
}
