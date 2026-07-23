using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// Metadata only (brief §4.1) — never republishes abstract/full text.
public class Publication(string openAlexId, string title, int year) : EntityBase, IAggregateRoot
{
  public string OpenAlexId { get; private set; } = Guard.Against.NullOrEmpty(openAlexId, nameof(openAlexId));
  public string Title { get; private set; } = Guard.Against.NullOrEmpty(title, nameof(title));
  public int Year { get; private set; } = year;
  public string? Doi { get; private set; }

  public void Rename(string title) => Title = Guard.Against.NullOrEmpty(title, nameof(title));
  public void SetYear(int year) => Year = year;
  public void SetDoi(string? doi) => Doi = doi;
}
