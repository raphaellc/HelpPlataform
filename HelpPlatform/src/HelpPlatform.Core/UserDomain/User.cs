using Ardalis.GuardClauses;
using HelpPlatform.Core.DonationRequestDomain;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.UserDomain;

public class User(string name, string email) : EntityBase, IAggregateRoot
{
  public string Name { get; private set; } = Guard.Against.NullOrWhiteSpace(name, nameof(name));
  
  public string Email { get; private set; } = Guard.Against.NullOrWhiteSpace(email, nameof(email));

  public DateTime CreatedAt { get; private set; } = DateTime.Now;
  
  public ICollection<DonationRequest> DonationRequests { get; private set; } = [];
  
  public void UpdateName(string name)
  {
      this.Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
  }
}
