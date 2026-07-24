using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Minimal by design (brief §3.2): name, affiliation via PublicationAuthor/ProjectParticipant links. No bios, no contact details.
public class Person(string fullName) : EntityBase, IAggregateRoot
{
  public string? Orcid { get; private set; }
  public string FullName { get; private set; } = Guard.Against.NullOrEmpty(fullName, nameof(fullName));
  public PersonStatus Status { get; private set; } = PersonStatus.Auto;

  public void LinkOrcid(string orcid) => Orcid = Guard.Against.NullOrEmpty(orcid, nameof(orcid));
  public void Rename(string fullName) => FullName = Guard.Against.NullOrEmpty(fullName, nameof(fullName));
  public void Suppress() => Status = PersonStatus.Suppressed;
  public void Remove() => Status = PersonStatus.Removed;
}
