using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Precomputed nightly from co-participation in ProjectParticipant. Not written by request handling.
public class CollabEdge(int orgA, int orgB, int weight, int firstYear, int lastYear, int projectCount) : EntityBase
{
  public int OrgA { get; private set; } = orgA;
  public int OrgB { get; private set; } = orgB;
  public int Weight { get; private set; } = weight;
  public int FirstYear { get; private set; } = firstYear;
  public int LastYear { get; private set; } = lastYear;
  public int ProjectCount { get; private set; } = projectCount;

  public void UpdateStats(int weight, int firstYear, int lastYear, int projectCount)
  {
    Weight = weight;
    FirstYear = firstYear;
    LastYear = lastYear;
    ProjectCount = projectCount;
  }
}
