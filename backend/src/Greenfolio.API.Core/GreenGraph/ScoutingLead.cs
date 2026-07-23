using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

// R1: scouting-as-a-service intake (brief §2). PO is notified same day per §6 EPIC C.
public class ScoutingLead(string name, string email, string company, string brief, DateTimeOffset consentAt) : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public string Email { get; private set; } = Guard.Against.NullOrEmpty(email, nameof(email));
  public string Company { get; private set; } = Guard.Against.NullOrEmpty(company, nameof(company));
  public string Brief { get; private set; } = Guard.Against.NullOrEmpty(brief, nameof(brief));
  public string? BudgetBand { get; private set; }
  public DateTimeOffset ConsentAt { get; private set; } = consentAt;
  public ScoutingLeadStatus Status { get; private set; } = ScoutingLeadStatus.New;
  public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

  public void SetBudgetBand(string budgetBand) => BudgetBand = Guard.Against.NullOrEmpty(budgetBand, nameof(budgetBand));
  public void MarkContacted() => Status = ScoutingLeadStatus.Contacted;
  public void MarkDelivered() => Status = ScoutingLeadStatus.Delivered;
  public void Close() => Status = ScoutingLeadStatus.Closed;
}
