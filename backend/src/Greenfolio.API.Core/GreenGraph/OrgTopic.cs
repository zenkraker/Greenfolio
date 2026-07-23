using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Aggregated from ProjectTopic + PublicationAuthor; drives search & profile pages. Rebuilt, not hand-edited.
public class OrgTopic(int orgId, int topicId, double score) : EntityBase
{
  public int OrgId { get; private set; } = orgId;
  public int TopicId { get; private set; } = topicId;
  public double Score { get; private set; } = score;

  public void UpdateScore(double score) => Score = score;
}
