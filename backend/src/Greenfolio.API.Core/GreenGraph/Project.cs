using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class Project(int sourceId, string sourceNativeId, string title, string sourceUrl) : EntityBase, IAggregateRoot
{
  public int SourceId { get; private set; } = sourceId;
  public string SourceNativeId { get; private set; } = Guard.Against.NullOrEmpty(sourceNativeId, nameof(sourceNativeId));
  public string Title { get; private set; } = Guard.Against.NullOrEmpty(title, nameof(title));
  public DateOnly? StartDate { get; private set; }
  public DateOnly? EndDate { get; private set; }
  public decimal? FundingAmount { get; private set; }
  public string? Currency { get; private set; }
  public string? Program { get; private set; }
  public string? OurSummary { get; private set; }
  public string SourceUrl { get; private set; } = Guard.Against.NullOrEmpty(sourceUrl, nameof(sourceUrl));

  public ICollection<ProjectParticipant> Participants { get; private set; } = new List<ProjectParticipant>();
  public ICollection<ProjectTopic> Topics { get; private set; } = new List<ProjectTopic>();

  public void UpdateTitle(string title) => Title = Guard.Against.NullOrEmpty(title, nameof(title));
  public void SetDates(DateOnly? startDate, DateOnly? endDate) { StartDate = startDate; EndDate = endDate; }
  public void SetFunding(decimal? amount, string? currency) { FundingAmount = amount; Currency = currency; }
  public void SetProgram(string? program) => Program = program;
  public void SetSummary(string? summary) => OurSummary = summary;
}
