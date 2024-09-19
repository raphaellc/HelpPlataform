using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.User.Interfaces;
using MediatR;

namespace HelpPlatform.Core.User.Services;

public class DeleteUserService(IRepository<User> _repository) : IDeleteUserService
{
    public async Task<Result> DeleteUser(int userId, CancellationToken cancellationToken)
    {
        var userToDelete = await _repository.GetByIdAsync(userId);

        if (userToDelete is null)
        {
            return Result.NotFound();
        }

        await _repository.DeleteAsync(userToDelete);
        return Result.Success();
    }
}
