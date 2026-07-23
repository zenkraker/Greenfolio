using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class Source(string key, string name, string licence, bool attributionRequired, string? url) : EntityBase, IAggregateRoot
{
  public string Key { get; private set; } = Guard.Against.NullOrEmpty(key, nameof(key));
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public string Licence { get; private set; } = Guard.Against.NullOrEmpty(licence, nameof(licence));
  public bool AttributionRequired { get; private set; } = attributionRequired;
  public string? Url { get; private set; } = url;
}
