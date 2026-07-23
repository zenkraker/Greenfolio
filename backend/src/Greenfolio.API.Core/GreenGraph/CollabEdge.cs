using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Precomputed nightly from co-participation in ProjectParticipant. Not written by request handling.
public class CollabEdge(int orgA, int orgB, int weight, int firstYear, int lastYear, int projectCount) : EntityBase
{
  public int OrgA { get; set; } = orgA;
  public int OrgB { get; set; } = orgB;
  public int Weight { get; set; } = weight;
  public int FirstYear { get; set; } = firstYear;
  public int LastYear { get; set; } = lastYear;
  public int ProjectCount { get; set; } = projectCount;
}
