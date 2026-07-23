using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class PublicationAuthor(int publicationId, int? personId, int? orgId) : EntityBase
{
  public int PublicationId { get; private set; } = publicationId;
  public int? PersonId { get; private set; } = personId;
  public int? OrgId { get; private set; } = orgId;
}
