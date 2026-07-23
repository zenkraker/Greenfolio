using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Not enumerated as its own table in brief §4.1, but "topics.field_id" implies one level above
// topic in the two-level taxonomy (§3.4) — added here to satisfy that FK. Fully immutable: a
// naming/structure change is a new row under a new TaxonomyVersion, not an in-place edit.
public class Field(string key, string name, int taxonomyVersion) : EntityBase, IAggregateRoot
{
  public string Key { get; private set; } = Guard.Against.NullOrEmpty(key, nameof(key));
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public int TaxonomyVersion { get; private set; } = taxonomyVersion;

  public ICollection<Topic> Topics { get; private set; } = new List<Topic>();
}
