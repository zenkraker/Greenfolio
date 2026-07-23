using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class ProjectParticipant(int projectId, int orgId, ParticipantRole role, int? personId) : EntityBase
{
  public int ProjectId { get; private set; } = projectId;
  public int OrgId { get; private set; } = orgId;
  public ParticipantRole Role { get; private set; } = role;
  public int? PersonId { get; private set; } = personId;
}
