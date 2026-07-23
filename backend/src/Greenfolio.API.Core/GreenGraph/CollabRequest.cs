using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class CollabRequest(string fromName, string fromEmail, string fromCompany, string description, DateTimeOffset consentAt) : EntityBase, IAggregateRoot
{
  public string FromName { get; private set; } = Guard.Against.NullOrEmpty(fromName, nameof(fromName));
  public string FromEmail { get; private set; } = Guard.Against.NullOrEmpty(fromEmail, nameof(fromEmail));
  public string FromCompany { get; private set; } = Guard.Against.NullOrEmpty(fromCompany, nameof(fromCompany));
  public int? TargetOrgId { get; set; }
  public int? TopicId { get; set; }
  public string Description { get; private set; } = Guard.Against.NullOrEmpty(description, nameof(description));
  public DateTimeOffset ConsentAt { get; private set; } = consentAt;
  public CollabRequestStatus Status { get; set; } = CollabRequestStatus.New;
  public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}
