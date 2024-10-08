using Ardalis.Result;

namespace HelpPlatform.Core.UserDomain.Interfaces;

public interface IDeleteUserService
{
  public Task<Result> DeleteUser(int userId, CancellationToken cancellationToken);
}
