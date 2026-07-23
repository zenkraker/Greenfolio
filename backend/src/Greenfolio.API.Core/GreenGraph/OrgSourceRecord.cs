using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Immutable: raw per-record import from a source. Never edited after insert, only superseded.
public class OrgSourceRecord(int? orgId, int sourceId, string sourceNativeId, string rawJson) : EntityBase
{
  public int? OrgId { get; private set; } = orgId;
  public int SourceId { get; private set; } = sourceId;
  public string SourceNativeId { get; private set; } = Guard.Against.NullOrEmpty(sourceNativeId, nameof(sourceNativeId));
  public string RawJson { get; private set; } = Guard.Against.NullOrEmpty(rawJson, nameof(rawJson));
  public DateTimeOffset ImportedAt { get; private set; } = DateTimeOffset.UtcNow;
}
