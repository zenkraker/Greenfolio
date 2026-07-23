using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class Project(int sourceId, string sourceNativeId, string title, string sourceUrl) : EntityBase, IAggregateRoot
{
  public int SourceId { get; private set; } = sourceId;
  public string SourceNativeId { get; private set; } = Guard.Against.NullOrEmpty(sourceNativeId, nameof(sourceNativeId));
  public string Title { get; set; } = Guard.Against.NullOrEmpty(title, nameof(title));
  public DateOnly? StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  public decimal? FundingAmount { get; set; }
  public string? Currency { get; set; }
  public string? Program { get; set; }
  public string? OurSummary { get; set; }
  public string SourceUrl { get; private set; } = Guard.Against.NullOrEmpty(sourceUrl, nameof(sourceUrl));

  public ICollection<ProjectParticipant> Participants { get; private set; } = new List<ProjectParticipant>();
  public ICollection<ProjectTopic> Topics { get; private set; } = new List<ProjectTopic>();
}
