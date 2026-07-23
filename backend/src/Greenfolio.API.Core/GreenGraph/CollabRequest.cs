using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class CollabRequest(string fromName, string fromEmail, string fromCompany, string description, DateTimeOffset consentAt) : EntityBase, IAggregateRoot
{
  public string FromName { get; private set; } = Guard.Against.NullOrEmpty(fromName, nameof(fromName));
  public string FromEmail { get; private set; } = Guard.Against.NullOrEmpty(fromEmail, nameof(fromEmail));
  public string FromCompany { get; private set; } = Guard.Against.NullOrEmpty(fromCompany, nameof(fromCompany));
  public int? TargetOrgId { get; private set; }
  public int? TopicId { get; private set; }
  public string Description { get; private set; } = Guard.Against.NullOrEmpty(description, nameof(description));
  public DateTimeOffset ConsentAt { get; private set; } = consentAt;
  public CollabRequestStatus Status { get; private set; } = CollabRequestStatus.New;
  public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

  public void SetTargetOrg(int orgId) => TargetOrgId = orgId;
  public void SetTopic(int topicId) => TopicId = topicId;
  public void Forward() => Status = CollabRequestStatus.Forwarded;
  public void MarkMatched() => Status = CollabRequestStatus.Matched;
  public void Close() => Status = CollabRequestStatus.Closed;
}
