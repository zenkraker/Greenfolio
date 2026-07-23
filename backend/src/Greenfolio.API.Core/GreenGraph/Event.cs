using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// First-party analytics event, cookieless (brief §3.2.6) — no cross-session identifiers.
// Write-once: fully specified at construction, never mutated after logging.
public class Event(EventType type, int? orgId = null, int? topicId = null, string? queryText = null, string? metaJson = null) : EntityBase, IAggregateRoot
{
  public DateTimeOffset OccurredAt { get; private set; } = DateTimeOffset.UtcNow;
  public EventType Type { get; private set; } = type;
  public int? OrgId { get; private set; } = orgId;
  public int? TopicId { get; private set; } = topicId;
  public string? QueryText { get; private set; } = queryText;
  public string? MetaJson { get; private set; } = metaJson;
}
