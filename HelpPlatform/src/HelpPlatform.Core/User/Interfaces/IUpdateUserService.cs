using Ardalis.Result;

namespace HelpPlatform.Core.User.Interfaces;

public interface IUpdateUserService
{
    public Task<Result> UpdateUser(int userId, string name, CancellationToken cancellationToken);
}
