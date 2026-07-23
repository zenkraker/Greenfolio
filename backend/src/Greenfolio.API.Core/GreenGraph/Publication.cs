using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Metadata only (brief §4.1) — never republishes abstract/full text.
public class Publication(string openAlexId, string title, int year) : EntityBase, IAggregateRoot
{
  public string OpenAlexId { get; private set; } = Guard.Against.NullOrEmpty(openAlexId, nameof(openAlexId));
  public string Title { get; set; } = Guard.Against.NullOrEmpty(title, nameof(title));
  public int Year { get; set; } = year;
  public string? Doi { get; set; }
}
