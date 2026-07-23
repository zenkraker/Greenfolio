using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Greenfolio.API.Core.GreenGraph;

public class User(string email, string passwordHash, UserRole role) : EntityBase, IAggregateRoot
{
  public string Email { get; private set; } = Guard.Against.NullOrEmpty(email, nameof(email));
  public string PasswordHash { get; private set; } = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
  public UserRole Role { get; set; } = role;
  public int? OrgId { get; set; }
}
