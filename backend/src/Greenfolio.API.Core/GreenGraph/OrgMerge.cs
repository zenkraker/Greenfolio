using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// A mapping, not an overwrite: source records for merged_org_id stay immutable and attributed.
public class OrgMerge(int keptOrgId, int mergedOrgId, OrgMergeMethod method, double confidence, string? decidedBy) : EntityBase
{
  public int KeptOrgId { get; private set; } = keptOrgId;
  public int MergedOrgId { get; private set; } = mergedOrgId;
  public OrgMergeMethod Method { get; private set; } = method;
  public double Confidence { get; private set; } = confidence;
  public string? DecidedBy { get; private set; } = decidedBy;
  public DateTimeOffset DecidedAt { get; private set; } = DateTimeOffset.UtcNow;
}
