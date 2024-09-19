using Ardalis.Result;

namespace HelpPlatform.Core.User.Interfaces;

public interface IDeleteUserService
{
  public Task<Result> DeleteUser(int userId, CancellationToken cancellationToken);
}
