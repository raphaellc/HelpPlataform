using Ardalis.Result;
using Ardalis.SharedKernel;
using HelpPlatform.Core.User.Interfaces;

namespace HelpPlatform.Core.User.Services;

public class UpdateUserService(IRepository<User> _repository) : IUpdateUserService
{
    public async Task<Result> UpdateUser(int userId, string name, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByIdAsync(userId);
        if (existingUser is null)
        {
            return Result.NotFound();
        }

        existingUser.UpdateName(name);
        
        await _repository.UpdateAsync(existingUser, cancellationToken);

        return Result.NoContent();
    }
}
