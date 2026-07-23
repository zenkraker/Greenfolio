using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class Claim(int orgId, int userId, ClaimMethod method) : EntityBase, IAggregateRoot
{
  public int OrgId { get; private set; } = orgId;
  public int UserId { get; private set; } = userId;
  public ClaimMethod Method { get; private set; } = method;
  public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
  public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}
