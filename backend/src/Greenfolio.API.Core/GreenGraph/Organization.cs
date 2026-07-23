using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class Organization(string name, string country, OrgType orgType) : EntityBase, IAggregateRoot
{
  public string? RorId { get; set; }
  public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public string[] NameVariants { get; set; } = [];
  public string Country { get; set; } = Guard.Against.NullOrEmpty(country, nameof(country));
  public OrgType OrgType { get; set; } = orgType;
  public double? Lat { get; set; }
  public double? Lng { get; set; }
  public string? Website { get; set; }
  public OrgStatus Status { get; set; } = OrgStatus.Auto;

  public ICollection<OrgSourceRecord> SourceRecords { get; private set; } = new List<OrgSourceRecord>();
  public ICollection<OrgTopic> Topics { get; private set; } = new List<OrgTopic>();
}
