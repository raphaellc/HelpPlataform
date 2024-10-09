using Ardalis.Result;
using HelpPlatform.Core.UserDomain.Interfaces;
using HelpPlatform.SharedKernel;

namespace HelpPlatform.Core.UserDomain.Services;

public class DeleteUserService(IRepository<User> repository) : IDeleteUserService
{
    public async Task<Result> DeleteUser(int userId, CancellationToken cancellationToken)
    {
        var userToDelete = await repository.GetByIdAsync(userId, cancellationToken);

        if (userToDelete is null)
        {
            return Result.NotFound();
        }

        await repository.DeleteAsync(userToDelete, cancellationToken);
        return Result.Success();
    }
}
