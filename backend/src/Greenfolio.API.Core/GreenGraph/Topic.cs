using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class Topic(int fieldId, string key, string name, int taxonomyVersion) : EntityBase, IAggregateRoot
{
  public int FieldId { get; private set; } = fieldId;
  public string Key { get; private set; } = Guard.Against.NullOrEmpty(key, nameof(key));
  public string Name { get; set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public int TaxonomyVersion { get; private set; } = taxonomyVersion;
}
