using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class Organization(string name, string country, OrgType orgType) : EntityBase, IAggregateRoot
{
  public string? RorId { get; private set; }
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public string[] NameVariants { get; private set; } = [];
  public string Country { get; private set; } = Guard.Against.NullOrEmpty(country, nameof(country));
  public OrgType OrgType { get; private set; } = orgType;
  public double? Lat { get; private set; }
  public double? Lng { get; private set; }
  public string? Website { get; private set; }
  public OrgStatus Status { get; private set; } = OrgStatus.Auto;

  public ICollection<OrgSourceRecord> SourceRecords { get; private set; } = new List<OrgSourceRecord>();
  public ICollection<OrgTopic> Topics { get; private set; } = new List<OrgTopic>();

  public void Rename(string name) => Name = Guard.Against.NullOrEmpty(name, nameof(name));

  public void AddNameVariant(string variant)
  {
    Guard.Against.NullOrEmpty(variant, nameof(variant));
    if (!NameVariants.Contains(variant)) NameVariants = [.. NameVariants, variant];
  }

  public void SetCountry(string country) => Country = Guard.Against.NullOrEmpty(country, nameof(country));
  public void SetOrgType(OrgType orgType) => OrgType = orgType;
  public void SetGeolocation(double lat, double lng) { Lat = lat; Lng = lng; }
  public void SetWebsite(string? website) => Website = website;
  public void LinkRor(string rorId) => RorId = Guard.Against.NullOrEmpty(rorId, nameof(rorId));

  public void MarkClaimed() => Status = OrgStatus.Claimed;
  public void Suppress() => Status = OrgStatus.Suppressed;
  public void Remove() => Status = OrgStatus.Removed;
}
