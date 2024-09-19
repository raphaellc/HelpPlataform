using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace HelpPlatform.Core.User;

public class User(string name, string email) : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = Guard.Against.NullOrWhiteSpace(name, nameof(name));

  public string Email { get; private set; } = email;

  public DateTime CreationTime { get; private set; }

  public void UpdateName(string name)
  {
      this.Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
  }
}
