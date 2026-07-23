using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Minimal by design (brief §3.2): name, affiliation via PublicationAuthor/ProjectParticipant links. No bios, no contact details.
public class Person(string fullName) : EntityBase, IAggregateRoot
{
  public string? Orcid { get; set; }
  public string FullName { get; set; } = Guard.Against.NullOrEmpty(fullName, nameof(fullName));
  public PersonStatus Status { get; set; } = PersonStatus.Auto;
}
