using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Fully immutable, same reasoning as Field: a change is a new row under a new TaxonomyVersion.
public class Topic(int fieldId, string key, string name, int taxonomyVersion) : EntityBase, IAggregateRoot
{
  public int FieldId { get; private set; } = fieldId;
  public string Key { get; private set; } = Guard.Against.NullOrEmpty(key, nameof(key));
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public int TaxonomyVersion { get; private set; } = taxonomyVersion;
}
