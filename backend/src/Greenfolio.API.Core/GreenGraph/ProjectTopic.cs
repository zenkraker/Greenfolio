using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class ProjectTopic(int projectId, int topicId, double confidence, string classifierVersion) : EntityBase
{
  public int ProjectId { get; private set; } = projectId;
  public int TopicId { get; private set; } = topicId;
  public double Confidence { get; private set; } = confidence;
  public string ClassifierVersion { get; private set; } = classifierVersion;
}
